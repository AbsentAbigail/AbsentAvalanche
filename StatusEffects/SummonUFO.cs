using AbsentAvalanche.Cards.Items;
using AbsentUtilities;
using Deadpan.Enums.Engine.Components.Modding;

namespace AbsentAvalanche.StatusEffects;

internal class SummonUFO() : AbstractStatus<StatusEffectSummon>(Name)
{
    public const string Name = "Summon UFO";

    public override StatusEffectDataBuilder Builder()
    {
        return AbsentUtils.StatusCopy("Summon Junk", Name)
            .SubscribeToAfterAllBuildEvent(data =>
            {
                var status = (StatusEffectSummon)data;
                status.summonCard = AbsentUtils.GetCard(RescueUFO.Name);
            });
    }
}