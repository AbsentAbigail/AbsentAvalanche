﻿using System.Linq;
using AbsentAvalanche.StatusEffects.Implementations;
using AbsentUtilities;

namespace AbsentAvalanche.StatusEffects;

public class InstantGainRandomCharm() : AbstractStatus<StatusEffectInstantAddRandomCharm>(
    Name,
    canBoost: true,
    subscribe: status =>
    {
        status.Predicate = charm => charm.scripts is null || charm.scripts.Count(s => s.GetType().Name != "CardScriptAddStatsWhenCharmed") == 0;
        status.addToTarget = true;
    })
{
    public const string Name = "Instant Gain Random Charm";
}