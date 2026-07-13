using System;
using System.Collections;

namespace AbsentAvalanche.StatusEffectImplementations;

public class StatusEffectInvulnerableWhileSnowed : StatusEffectData
{
    private readonly StatusEffectData _snow = Absent.GetStatus("Snow");
    private bool _isInvisible;
    
    public override void Init()
    {
        OnStack += Start;
        OnApplyStatus += IncreaseInvulnerable;
        OnTurnEnd += EndTurn;
    }

    private IEnumerator Start(int stacks)
    {
        if (target.FindStatus(_snow.type))
        {
            MakeInvisible();
        }
        yield break;
    }

    public override bool RunApplyStatusEvent(StatusEffectApply apply)
    {
        return apply.target == target && apply.effectData.type == _snow.type && !target.FindStatus(_snow.type);
    }
    
    public override bool RunTurnEndEvent(Entity entity)
    {
        return entity == References.Player.entity;
    }

    private IEnumerator EndTurn(Entity entity)
    {
        if (target.FindStatus(_snow.type))
        {
            yield break;
        }

        MakeVisible();
    }
    
    private IEnumerator IncreaseInvulnerable(StatusEffectApply apply)
    {
        MakeInvisible();
        yield break;
    }

    private void MakeInvisible()
    {
        if (_isInvisible)
        {
            return;
        }
        ChangeAlpha(0.5f);
        target.cannotBeHitCount += 1;
        _isInvisible = true;
    }
    
    private void MakeVisible()
    {
        if (!_isInvisible)
        {
            return;
        }
        ChangeAlpha(1f);
        target.cannotBeHitCount -= 1;
        _isInvisible = false;
    }
    
    private void ChangeAlpha(float alpha)
    {
        ((Card)target.display).canvasGroup.alpha = alpha;
    }
}