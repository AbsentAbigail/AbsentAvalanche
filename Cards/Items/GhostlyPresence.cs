﻿using AbsentAvalanche.StatusEffects;
using AbsentAvalanche.Traits;
using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.Cards.Items;

internal class GhostlyPresence() : AbstractItem(
    Name, "Ghostly Presence",
    pools: Pools.Shademancer,
    subscribe: card =>
    {
        card.traits = [AbsentUtils.TStack(Rest.Name, 3)];
        card.startWithEffects =
        [
            AbsentUtils.SStack(Ethereal.Name, 3),
            AbsentUtils.SStack(WhileInHandApplyOverburnToRandomEnemy.Name, 2)
        ];
    })
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}