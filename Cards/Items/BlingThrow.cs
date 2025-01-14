using AbsentAvalanche.Traits;
using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.Cards.Items;

internal class BlingThrow() : AbstractItem(
    Name, "Bling Throw",
    5, true,
    playOnHand: false,
    subscribe: card =>
    {
        card.traits = [AbsentUtils.TStack(GoldRush.Name)];
        card.startWithEffects =
        [
            AbsentUtils.SStack("Pre Turn Take Gold", 2),
            AbsentUtils.SStack("On Kill Apply Gold To Self", 2)
        ];
    })
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}