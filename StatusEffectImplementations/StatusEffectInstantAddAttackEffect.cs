using System.Collections;

namespace AbsentAvalanche.StatusEffectImplementations;

public class StatusEffectInstantAddAttackEffect : StatusEffectInstant
{
    public StatusEffectData effectToApply;
    
    public override IEnumerator Process()
    {
        AddAttackEffect(GetAmount());
        target.display.promptUpdateDescription = true;
        target.PromptUpdate();
        yield return Remove();
    }
    
    private void AddAttackEffect(int amount)
    {
        target.attackEffects = CardData.StatusEffectStacks.Stack(
            target.attackEffects,
            [new CardData.StatusEffectStacks(
                effectToApply,
                amount)]).ToList();
    }
}