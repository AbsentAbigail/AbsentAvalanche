using Deadpan.Enums.Engine.Components.Modding;

namespace AbsentAvalanche.StatusEffects
{
    internal class InstantSummonUnboundFlame() : AbstractStatus<StatusEffectInstantSummon>(Name)
    {
        public const string Name = "Instant Summon Unbound Flame";

        public override StatusEffectDataBuilder Builder()
        {
            return Absent.StatusCopy("Instant Summon Dregg", Name)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    var status = (StatusEffectInstantSummon)data;
                    status.targetSummon = Absent.TryGet<StatusEffectSummon>(SummonUnboundFlame.Name);
                });
        }
    }
}