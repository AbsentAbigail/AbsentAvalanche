using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AbsentAvalanche.GameSystems;
using Deadpan.Enums.Engine.Components.Modding;
using UnityEngine;
using UnityEngine.Localization;

namespace AbsentAvalanche.StatusEffectImplementations;

public class StatusEffectMetronome : StatusEffectInstantApplyEffect
{
    public Move[] movePool;
    public Move[] criticalMovePool;
    public int criticalOdds = 16;

    public override IEnumerator Process()
    {
        yield return CustomTextPopupSystem.Run(target, GetLocalizedString("Metronome"), target.data.title, "Metronome");
        var crit = criticalOdds > 0 && Random.Range(0, criticalOdds) == 0;
        
        var metronome = Absent.GetStatusOf<StatusEffectMetronome>(name); // The game does not properly copy struct arrays so we have to get the pools from the original
        var pool = (crit ? metronome.criticalMovePool : metronome.movePool).ToList();
        
        pool.RemoveAllWhere(move => !ValidMove(move));
        
        if (!pool.Any())
        {
            yield return CustomTextPopupSystem.Run(target, GetLocalizedString("Splash"), target.data.title);
            yield return Remove();
            yield break;
        }

        var selectedMove = pool.ToArray().RandomItem();
        yield return CustomTextPopupSystem.Run(target, GetLocalizedString("Metronome"), target.data.title, selectedMove.name);
        if (crit)
        {
            yield return CustomTextPopupSystem.Run(target, GetLocalizedString("CriticalHit"));
        }

        FindObjectOfType<BattleLogSystem>()?.Log(GetLocalizedString("MetronomeBattleLog"),
            BattleLogType.Buff, BattleLogSystem.GetBattleEntity(target), selectedMove.name, selectedMove.Description());
        
        count = selectedMove.count;
        foreach (var selectedMoveEffect in selectedMove.effects)
        {
            effectToApply = selectedMoveEffect;
            yield return Run();
        }
        if (selectedMove.increaseAttack > 0)
        {
            target.damage.current += selectedMove.increaseAttack;
        }
        target.display.promptUpdateDescription = true;
        target.PromptUpdate();
        yield return Remove();
    }

    private IEnumerator Run()
    {
        var amount = (bool) (Object) scriptableAmount ? scriptableAmount.Get(target) : GetAmount();
        yield return StatusEffectSystem.Apply(target, applier, effectToApply, amount);
    }

    private bool ValidMove(Move move)
    {
        // Debug overwrite
        // return move.name == "Leech Seed";
        
        var hasAttack = target.HasAttackIcon();
        if (move.increaseAttack > 0 && !hasAttack)
        {
            return false;
        }

        return move.effects.All(statusEffectData => statusEffectData.targetConstraints.All(constraint => constraint.Check(target)));
    }
    
    private static LocalizedString GetLocalizedString(string localisedName)
    {
        return LocalizationHelper.GetCollection("UI Text", SystemLanguage.English).GetString(localisedName);
    }

    public struct Move
    {
        public StatusEffectData[] effects;
        public int count;
        public string name;
        public string description;
        public int increaseAttack;

        public string Description()
        {
            var descriptionParts = new List<string>();
            descriptionParts.AddIfNotNull(increaseAttack > 0 ? "[+{1}]<sprite name=attack>" : null);
            descriptionParts.AddIfNotNull(description);
            return string.Join(", ", descriptionParts).Format(count, increaseAttack);
        }
    }
}