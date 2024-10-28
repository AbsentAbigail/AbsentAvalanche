using AbsentAvalanche.Cards.Companion;
using AbsentUtilities;
using Deadpan.Enums.Engine.Components.Modding;

namespace AbsentAvalanche.StatusEffects;

internal class WhenDestroyedSummonUnboundFlame() : AbstractStatus<StatusEffectData>(Name)
{
    public const string Name = "When Destroyed Summon Unbound Flame";

    public override StatusEffectDataBuilder Builder()
    {
        return AbsentUtils.StatusCopy("When Destroyed Summon Dregg", Name)
            .WithTextInsert(AbstractCard.CardTag(UnboundFlame.Name))
            .SubscribeToAfterAllBuildEvent(data =>
            {
                var status = (StatusEffectApplyXWhenDestroyed)data;
                status.effectToApply = AbsentUtils.GetStatus(InstantSummonUnboundFlame.Name);
            });
    }
}