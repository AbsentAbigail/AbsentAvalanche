using AbsentAvalanche.StatusEffects;
using AbsentUtilities;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;

namespace AbsentAvalanche.Cards.Items;

internal class NebulaAuxilium() : AbstractItem(
    Name, "Nebula Auxilium",
    pools: Pools.None,
    subscribe: card =>
    {
        card.AddToPets();
        card.traits = [
            AbsentUtils.TStack("Zoomlin"),
            AbsentUtils.TStack("Consume"),
        ];
        card.startWithEffects =
        [
            AbsentUtils.SStack(OnCardPlayedTutorRandomCompanion.Name)
        ];
        card.greetMessages =
        [
            "As you stare into the void, the void stares back"
        ];
    })
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public override string FlavourText => "Space donut";
}