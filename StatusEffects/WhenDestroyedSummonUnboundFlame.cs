using AbsentAvalanche.Cards;
using AbsentAvalanche.Cards.Companion;
using Deadpan.Enums.Engine.Components.Modding;

namespace AbsentAvalanche.StatusEffects
{
    internal class WhenDestroyedSummonUnboundFlame() : AbstractStatus<StatusEffectInstantSummon>(Name)
    {
        public const string Name = "When Destroyed Summon Unbound Flame";

        public override StatusEffectDataBuilder Builder()
        {
            return Absent.StatusCopy("When Destroyed Summon Dregg", Name)
                .WithTextInsert(CardHelper.CardTag(UnboundFlame.Name))
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    var status = (StatusEffectApplyXWhenDestroyed)data;
                    status.effectToApply = Absent.TryGet<StatusEffectData>(InstantSummonUnboundFlame.Name);
                });
        }
    }
}