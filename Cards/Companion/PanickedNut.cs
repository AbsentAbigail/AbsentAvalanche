using AbsentAvalanche.StatusEffects;
using AbsentUtilities;
using Deadpan.Enums.Engine.Components.Modding;

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
    })
{
    public const string Name = "PanickedNut";
    public override string FlavourText => "Won't leave its shell";
    protected override string IdleAnimation => "ShakeAnimationProfile";
    protected override string BloodProfile => "Blood Profile Husk";
}