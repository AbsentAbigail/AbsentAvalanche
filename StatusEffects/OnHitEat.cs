﻿using AbsentAvalanche.StatusEffects.Implementations;
using AbsentUtilities;

namespace AbsentAvalanche.StatusEffects;

internal class OnHitEat() : AbstractApplyXStatus<StatusOnHitEat>(
    Name, "Eat and <keyword=absorb> targets with less <keyword=health> than my <keyword=attack>",
    effectToApply: InstantEat.Name,
    applyToFlags: StatusEffectApplyX.ApplyToFlags.Target
)
{
    public const string Name = "On Hit Eat";
}