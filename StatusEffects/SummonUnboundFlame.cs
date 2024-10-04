using AbsentAvalanche.Cards.Companion;
using Deadpan.Enums.Engine.Components.Modding;

namespace AbsentAvalanche.StatusEffects
{
    internal class SummonUnboundFlame() : AbstractStatus<StatusEffectSummon>(Name)
    {
        public const string Name = "Summon Unbound Flame";
        public override StatusEffectDataBuilder Builder()
        {
            return Absent.StatusCopy("Summon Dregg", Name)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    var status = (StatusEffectSummon)data;
                    status.summonCard = Absent.TryGet<CardData>(UnboundFlame.Name);
                });
        }
    }
}