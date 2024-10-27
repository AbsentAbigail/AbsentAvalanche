using System.Collections;

namespace AbsentAvalanche.StatusEffects.Implementations;

internal class StatusEffectAbduct : StatusEffectApplyX
{
    public override void Init()
    {
        OnStack += Apply;
        OnTurnEnd += EndTurn;
        OnHit += PreventDamage;
    }

    public override bool RunHitEvent(Hit hit)
    {
        return hit.target == target;
    }

    private static IEnumerator PreventDamage(Hit hit)
    {
        hit.damage = 0;
        yield break;
    }

    public override bool RunTurnEndEvent(Entity entity)
    {
        return entity == target;
    }

    private IEnumerator Apply(int stacks)
    {
        ChangeAlpha(0.5f);
        target.cannotBeHitCount += stacks;
        yield return Run(GetTargets(), 1);
    }

    private IEnumerator EndTurn(Entity entity)
    {
        ChangeAlpha(1f);
        target.cannotBeHitCount -= count;
        yield return Remove();

        target.display.promptUpdateDescription = true;
        target.PromptUpdate();
    }

    private void ChangeAlpha(float alpha)
    {
        ((Card)target.display).canvasGroup.alpha = alpha;
    }
}