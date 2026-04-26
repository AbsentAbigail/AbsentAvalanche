#region

using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Helpers;
using AbsentAvalanche.StatusEffectImplementations;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Builders.StatusEffects;

[UsedImplicitly]
public class WhenEquipeeDiesGainHalfAttack : IStatusBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Absent.Instance)
            .Create<StatusEffectApplyWhenEquipeeDies>(Name)
            .WithText($"When my wielder {Absent.KeywordTag(Keywords.Equip.Name)} dies, I gain half of its <keyword=attack>")
            .WithStackable(false)
            .WithCanBeBoosted(false)
            .SubscribeToAfterAllBuildEvent<StatusEffectApplyWhenEquipeeDies>(status =>
            {
                status.targetConstraints = [TargetConstraintHelper.IsCardType(["Item"])];
                status.scriptableAmount = new Script<ScriptableCurrentAttack>("Half attack", s =>
                {
                    s.multiplier = 0.5f;
                    s.roundUp = true;
                });
                status.applyEqualAmount = true;
                status.effectToApply = Absent.GetStatus("Increase Attack");
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.Self;
            });
    }
}