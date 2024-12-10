using AbsentAvalanche.StatusEffects;
using AbsentUtilities;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;

namespace AbsentAvalanche.Cards.Companion;

internal class PanickedNut() : AbstractCompanion(
    Name, "Panicked Nut",
    3, 1, 3,
    Pools.Snowdweller,
    card =>
    {
        card.startWithEffects =
        [
            AbsentUtils.SStack(WhenDeployedGainShellForEachEnemy.Name, 3),
            AbsentUtils.SStack(WhenDeployedGainSnowForEachEnemy.Name),
            AbsentUtils.SStack("Teeth")
        ];
        card.greetMessages =
        [
            "Please don't make me fight..."
        ];
    })
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public override string FlavourText => "Won't leave its shell";
    protected override string IdleAnimation => "ShakeAnimationProfile";
    protected override string BloodProfile => "Blood Profile Husk";
}