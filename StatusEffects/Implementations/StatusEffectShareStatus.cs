using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AbsentUtilities;
using UnityEngine;

namespace AbsentAvalanche.StatusEffects.Implementations;

// Code heavily inspired from Pokefrosts Synchronise
public class StatusEffectShareStatus : StatusEffectApplyX
{
    private int _chain;
    private const int MaxChain = 3;
    public ApplyToFlags whenAppliedFlags;
    public bool negativeStatus = true;
    public bool positiveStatus = true;
    public string[] whenAppliedTypes = [];

    private readonly Dictionary<string, Vector2Int> _amounts = new();

    public override void Init()
    {
        PostApplyStatus += Share;
    }

    public override bool RunApplyStatusEvent(StatusEffectApply apply)
    {
        if (!apply.applier || apply.applier == target || !apply.target || !apply.effectData ||
            apply.effectData.type.IsNullOrWhitespace() || target.silenced)
            return false;
        
        if (!CheckType(apply.effectData))
            return false;

        if (!CheckTarget(apply.target))
            return false;

        _amounts[apply.effectData.type] = CurrentAmounts(apply.target, apply.effectData.type);
        return false;
    }

    public override bool RunPostApplyStatusEvent(StatusEffectApply apply)
    {
        if (!apply.applier || apply.applier == target || !apply.target || !apply.effectData ||
            apply.effectData.type.IsNullOrWhitespace() || target.silenced)
            return false;

        if (!CheckType(apply.effectData))
            return false;

        if (!CheckTarget(apply.target))
            return false;

        if (!_amounts.TryGetValue(apply.effectData.type, out var amount))
            return false;

        var newAmount = CurrentAmounts(apply.target, apply.effectData.type);
        if (newAmount.x - amount.x - (newAmount.y - amount.y) <= 0 && newAmount.x - amount.x != 0)
            return false;

        _amounts.Remove(apply.effectData.type);
        return true;
    }

    private IEnumerator Share(StatusEffectApply apply)
    {
        _chain++;
        if (_chain >= MaxChain)
            yield break;
        
        effectToApply = apply.effectData;
        var targets = GetTargets();
        targets.Remove(apply.target);
        
        yield return Run(targets, apply.count);
        _chain = 0;
    }

    private bool CheckTarget(Entity entity)
    {
        var apply = applyToFlags;
        applyToFlags = whenAppliedFlags;
        var entities = GetTargets();
        applyToFlags = apply;
        return entities.Contains(entity);
    }

    private static Vector2Int CurrentAmounts(Entity frontAlly, string effectType)
    {
        var effect = frontAlly.statusEffects.FirstOrDefault(s => s.type == effectType);
        return effect == default(StatusEffectData) ? Vector2Int.zero : new Vector2Int(effect.count, effect.temporary);
    }

    private bool CheckType(StatusEffectData effectData)
    {
        if (!effectData.isStatus)
            return false;
        
        if (!(negativeStatus == effectData.IsNegativeStatusEffect() ||
              positiveStatus != effectData.IsNegativeStatusEffect()))
            return false;
        
        return whenAppliedTypes.Length == 0 || whenAppliedTypes.Contains(effectData.type);
    }
}