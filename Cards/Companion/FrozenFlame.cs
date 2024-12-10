using AbsentAvalanche.StatusEffects;
using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.Cards.Companion;

internal class FrozenFlame() : AbstractCompanion(
    Name, "Frozen Flame",
    1, null, 1,
    Pools.Shademancer,
    card =>
    {
        card.startWithEffects =
        [
            AbsentUtils.SStack(OnCardPlayedGainOverload.Name),
            AbsentUtils.SStack("Block", 4),
            AbsentUtils.SStack(WhenDestroyedSummonUnboundFlame.Name)
        ];
        card.greetMessages =
        [
            "This damned frost... Hey you, help me out!",
            "Is it cold here, or is that just me?",
            "Who touched the thermostat?!"
        ];
    })
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public override string FlavourText => "How did this even happen?";
    protected override string BloodProfile => "Blood Profile Black";
    protected override string IdleAnimation => "FloatAnimationProfile";
}