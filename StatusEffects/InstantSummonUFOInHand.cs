using AbsentUtilities;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;

namespace AbsentAvalanche.StatusEffects;

internal class InstantSummonUfoInHand() : AbstractStatus<StatusEffectInstantSummon>(Name)
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public override StatusEffectDataBuilder Builder()
    {
        return AbsentUtils.StatusCopy("Instant Summon Junk In Hand", Name)
            .SubscribeToAfterAllBuildEvent(data =>
            {
                var status = (StatusEffectInstantSummon)data;
                status.targetSummon = AbsentUtils.GetStatus(SummonUfo.Name) as StatusEffectSummon;
            });
    }
}