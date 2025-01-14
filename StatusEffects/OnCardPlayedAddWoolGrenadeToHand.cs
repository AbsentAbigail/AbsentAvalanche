using AbsentAvalanche.Cards.Items;
using AbsentUtilities;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;

namespace AbsentAvalanche.StatusEffects;

internal class OnCardPlayedAddWoolGrenadeToHand() : AbstractStatus<StatusEffectData>(Name)
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public override StatusEffectDataBuilder Builder()
    {
        return AbsentUtils.StatusCopy("On Card Played Add Junk To Hand", Name)
            .WithText("Add <{a}> {0} to hand")
            .WithTextInsert(AbstractCard.CardTag(WoolGrenade.Name))
            .SubscribeToAfterAllBuildEvent(data =>
                ((StatusEffectApplyX)data).effectToApply = AbsentUtils.GetStatus(InstantSummonWoolGrenadeInHand.Name));
    }
}