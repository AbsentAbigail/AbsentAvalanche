﻿using AbsentAvalanche.StatusEffects;
using AbsentUtilities;
using Deadpan.Enums.Engine.Components.Modding;

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
    public const string Name = "NebulaAuxilium";
    public override string FlavourText => "Space donut";
}