using AbsentAvalanche.StatusEffects;
using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.Cards.Items;

internal class HappyDreams() : AbstractItem(
    Name, "Happy Dreams",
    subscribe: card =>
    {
        card.startWithEffects =
        [
            AbsentUtils.SStack(OnCardPlayedTutorDeckCopyConsumeZoomlin.Name)
        ];
    })
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public override string FlavourText => "Dreams are the fuel of the future";
}