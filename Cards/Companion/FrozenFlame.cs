using AbsentAvalanche.StatusEffects;
using AbsentUtilities;
using Deadpan.Enums.Engine.Components.Modding;

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
    })
{
    public const string Name = "FrozenFlame";
    public override string FlavourText => "How did this even happen?";
    protected override string BloodProfile => "Blood Profile Black";
    protected override string IdleAnimation => "FloatAnimationProfile";
}