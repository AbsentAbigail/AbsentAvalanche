using AbsentAvalanche.Cards.Companion;
using AbsentUtilities;
using Deadpan.Enums.Engine.Components.Modding;

namespace AbsentAvalanche.StatusEffects;

public class SummonBlahaj() : AbstractStatus<StatusEffectData>(Name)
{
    public const string Name = "Summon Blahaj";
    public override StatusEffectDataBuilder Builder()
    {
        return AbsentUtils.StatusCopy("Summon Dregg", Name)
            .SubscribeToAfterAllBuildEvent(data =>
            {
                var status = (StatusEffectSummon)data;
                status.summonCard = AbsentUtils.GetCard(Blahaj.Name);
            });
    }
}