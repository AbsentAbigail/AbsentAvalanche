using AbsentAvalanche.Cards.Items;
using AbsentUtilities;
using Deadpan.Enums.Engine.Components.Modding;

namespace AbsentAvalanche.StatusEffects;

internal class OnCardPlayedAddCatomicBombToHand() : AbstractStatus<StatusEffectData>(Name)
{
    public const string Name = "On Card Played Add Catomic Bomb To Hand";

    public override StatusEffectDataBuilder Builder()
    {
        return AbsentUtils.StatusCopy("On Card Played Add Junk To Hand", Name)
            .WithText("Add <{a}> {0} to hand")
            .WithTextInsert(AbstractCard.CardTag(CatomicBomb.Name))
            .SubscribeToAfterAllBuildEvent(data =>
                ((StatusEffectApplyX)data).effectToApply = AbsentUtils.GetStatus(InstantSummonCatomicBombInHand.Name));
    }
}