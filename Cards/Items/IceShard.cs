using AbsentAvalanche.StatusEffects;
using AbsentUtilities;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;

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
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    protected override string IdleAnimation => "Heartbeat2AnimationProfile";
}