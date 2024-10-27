using AbsentUtilities;
using Deadpan.Enums.Engine.Components.Modding;

namespace AbsentAvalanche.StatusEffects;

internal class InstantSummonUFOInHand() : AbstractStatus<StatusEffectInstantSummon>(Name)
{
    public const string Name = "Instant Summon UFO In Hand";

    public override StatusEffectDataBuilder Builder()
    {
        return AbsentUtils.StatusCopy("Instant Summon Junk In Hand", Name)
            .SubscribeToAfterAllBuildEvent(data =>
            {
                var status = (StatusEffectInstantSummon)data;
                status.targetSummon = AbsentUtils.GetStatus(SummonUFO.Name) as StatusEffectSummon;
            });
    }
}