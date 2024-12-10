using AbsentAvalanche.StatusEffects;
using AbsentUtilities;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;

namespace AbsentAvalanche.Cards.Companion;

internal class SalvoKitty() : AbstractCompanion(
    Name, "Salvo Kitty",
    4, 0, 3,
    Pools.None,
    card =>
    {
        card.AddToPets();
        card.startWithEffects =
        [
            AbsentUtils.SStack(Cat.Name),
            AbsentUtils.SStack(GainCatWhenMissileIsPlayed.Name),
            AbsentUtils.SStack(OnCardPlayedAddMissileToHand.Name, 2),
        ];
    })
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public override string FlavourText => "\"Who gave this cat this button?\"";
}