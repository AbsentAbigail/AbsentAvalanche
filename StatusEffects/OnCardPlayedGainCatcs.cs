using AbsentUtilities;

namespace AbsentAvalanche.StatusEffects;

internal class OnCardPlayedGainCat() : AbstractApplyXStatus<StatusEffectApplyXOnCardPlayed>(
    Name, "Gain <{a}>{0}",
    canBoost: true,
    effectToApply: Cat.Name,
    applyToFlags: StatusEffectApplyX.ApplyToFlags.Self,
    subscribe: status => { status.textInsert = Keywords.Cat.Tag; })
{
    public const string Name = "On Card Played Gain Cat";
}