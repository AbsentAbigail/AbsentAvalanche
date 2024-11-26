using AbsentUtilities;
using Deadpan.Enums.Engine.Components.Modding;

namespace AbsentAvalanche.StatusEffects;

public class InstantSummonBlahaj() : AbstractStatus<StatusEffectData>(Name)
{
    public const string Name = "Instant Summon Blahaj";

    public override StatusEffectDataBuilder Builder()
    {
        return AbsentUtils.StatusCopy("Instant Summon Dregg", Name)
            .SubscribeToAfterAllBuildEvent(data =>
            {
                var status = (StatusEffectInstantSummon)data;
                status.targetSummon = AbsentUtils.GetStatusOf<StatusEffectSummon>(SummonBlahaj.Name);
            });
    }
}