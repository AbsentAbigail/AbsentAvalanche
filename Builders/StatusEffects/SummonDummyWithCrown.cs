using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.StatusEffectImplementations;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

namespace AbsentAvalanche.Builders.StatusEffects;

[UsedImplicitly]
public class SummonDummyWithCrown : IStatusBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Absent.Instance)
            .Create<StatusEffectSummonWithCrown>(Name)
            .WithStackable(false)
            .WithCanBeBoosted(false)
            .SubscribeToAfterAllBuildEvent<StatusEffectSummonWithCrown>(status =>
            {
                var summon = Absent.GetStatusOf<StatusEffectSummon>("Summon Junk");
                status.summonCard = summon.summonCard;
                status.effectPrefabRef = summon.effectPrefabRef;
                status.toSummon = summon.toSummon;
            });
    }
}