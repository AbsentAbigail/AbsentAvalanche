using AbsentUtilities;
using Deadpan.Enums.Engine.Components.Modding;

namespace AbsentAvalanche.StatusEffects;

internal class InstantSummonPillowInHand() : AbstractStatus<StatusEffectData>(Name)
{
    public const string Name = "InstantSummonPillowInHand";

    public override StatusEffectDataBuilder Builder()
    {
        return AbsentUtils.StatusCopy("Instant Summon Junk In Hand", Name)
            .SubscribeToAfterAllBuildEvent(data =>
                ((StatusEffectInstantSummon)data).targetSummon =
                AbsentUtils.GetStatusOf<StatusEffectSummon>(SummonPillow.Name));
    }
}