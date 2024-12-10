using AbsentAvalanche.StatusEffects;
using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.Cards.Companion;

internal class UnboundFlame() : AbstractCompanion(
    Name, "Unbound Flame",
    5, 0, 3,
    Pools.None,
    card =>
    {
        card.attackEffects = [AbsentUtils.SStack("Overload", 3)];
        card.startWithEffects =
        [
            AbsentUtils.SStack(OnCardPlayedApplyOverloadToAlliesInRow.Name, 3)
        ];
        card.traits = [AbsentUtils.TStack("Barrage")];
        card.cardType = AbsentUtils.GetCardType("Summoned");
    })
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public override string FlavourText => "Ethereal flames fill the space around";
    protected override string BloodProfile => "Blood Profile Black";
    protected override string IdleAnimation => "FloatAnimationProfile";
}