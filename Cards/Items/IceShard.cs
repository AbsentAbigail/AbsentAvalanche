using AbsentAvalanche.StatusEffects;
using AbsentUtilities;
using Deadpan.Enums.Engine.Components.Modding;

namespace AbsentAvalanche.Cards.Items;

internal class IceShard() : AbstractItem(
    Name, "Ice Shard",
    1,
    subscribe: card =>
    {
        card.attackEffects = [AbsentUtils.SStack("Frost")];
        card.startWithEffects = [AbsentUtils.SStack(HitsAllAlliesAndEnemies.Name)];
    })
{
    public const string Name = "IceShard";
    protected override string IdleAnimation => "Heartbeat2AnimationProfile";
}