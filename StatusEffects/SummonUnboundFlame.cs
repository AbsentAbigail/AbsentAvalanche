using AbsentAvalanche.Cards.Companion;
using AbsentUtilities;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;

namespace AbsentAvalanche.StatusEffects;

internal class SummonUnboundFlame() : AbstractStatus<StatusEffectSummon>(Name)
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public override StatusEffectDataBuilder Builder()
    {
        return AbsentUtils.StatusCopy("Summon Dregg", Name)
            .SubscribeToAfterAllBuildEvent(data =>
            {
                var status = (StatusEffectSummon)data;
                status.summonCard = AbsentUtils.GetCard(UnboundFlame.Name);
            });
    }
}