using AbsentAvalanche.StatusEffects;
using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.Cards.Items;

internal class RescueUFO() : AbstractItem(
    Name, "Rescue UFO",
    needsTarget: true,
    shopPrice: 40,
    playOnHand: false,
    subscribe: card =>
    {
        card.attackEffects = [AbsentUtils.SStack(Abduct.Name)];
        card.traits =
        [
            AbsentUtils.TStack("Consume"),
            AbsentUtils.TStack("Zoomlin")
        ];
    })
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}