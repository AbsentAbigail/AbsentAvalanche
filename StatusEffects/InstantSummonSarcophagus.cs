using AbsentUtilities;

namespace AbsentAvalanche.StatusEffects;

public class InstantSummonSarcophagus(): AbstractStatus<StatusEffectInstantSummon>(
    Name, subscribe: status =>
    {
        status.canSummonMultiple = true;
        status.summonPosition = StatusEffectInstantSummon.Position.Hand;
        status.targetSummon = AbsentUtils.GetStatusOf<StatusEffectSummon>(SummonSarcophagus.Name);
    })
{
    public const string Name = "Instant Summon Sarcophagus";
}