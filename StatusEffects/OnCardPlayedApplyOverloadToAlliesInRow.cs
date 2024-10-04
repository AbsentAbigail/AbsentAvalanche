using static StatusEffectApplyX;

namespace AbsentAvalanche.StatusEffects
{
    internal class OnCardPlayedApplyOverloadToAlliesInRow() : AbstractApplyXStatus<StatusEffectApplyXOnCardPlayed>(
        Name, "Apply <{a}><keyword=overload> to allies in row",
        true, true,
        "Overload",
        ApplyToFlags.AlliesInRow)
    {
        public const string Name = "On Card Played Apply Overload To AlliesInRow";
    }
}