using AbsentUtilities;
using Deadpan.Enums.Engine.Components.Modding;

namespace AbsentAvalanche.StatusEffects;

public class InstantSummonDummyToHand() : AbstractStatus<StatusEffectData>(Name)
{
    public const string Name = "InstantSummonDummyToHand";
    
    public override StatusEffectDataBuilder Builder()
    {
        return AbsentUtils.StatusCopy("Instant Summon Junk In Hand", Name)
            .SubscribeToAfterAllBuildEvent(data =>
            {
                var status = ((StatusEffectInstantSummon)data);
                status.targetSummon = AbsentUtils.GetStatusOf<StatusEffectSummon>(DummySummon.Name);
            });
    }
}