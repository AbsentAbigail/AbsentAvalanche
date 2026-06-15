using AbsentAvalanche.Builders.Cards.Companions;
using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.StatusEffectImplementations;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

namespace AbsentAvalanche.Builders.StatusEffects;

[UsedImplicitly]
public class InstantSummonFromReserve : IStatusBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Absent.Instance)
            .Create<StatusEffectInstantSummonFromReserve>(Name)
            .WithStackable(true)
            .WithCanBeBoosted(false)
            .SubscribeToAfterAllBuildEvent<StatusEffectInstantSummonFromReserve>(status =>
            {
                status.targetSummon = Absent.GetStatusOf<StatusEffectSummon>(SummonReserve.Name);
                status.backupSummon = Absent.GetCard(EmptySeat.Name);
                status.withEffects =
                [
                    Absent.GetStatus("Temporary Summoned")
                ];
            });
    }
}