using static StatusEffectApplyX;

namespace AbsentAvalanche.StatusEffects
{
    internal class OnCardPlayedGainOverload() : AbstractApplyXStatus<StatusEffectApplyXOnCardPlayed>(
        Name, "Gain <{a}><keyword=overload>",
        true, true,
        "Overload", ApplyToFlags.Self
        )
    {
        public const string Name = "On Turn Gain Overload";
    }
}