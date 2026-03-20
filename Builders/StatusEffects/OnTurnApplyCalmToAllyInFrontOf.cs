#region

using AbsentAvalanche.Builders.Interfaces;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Builders.StatusEffects;

[UsedImplicitly]
public class OnTurnApplyCalmToAllyInFrontOf : IStatusBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Absent.Instance)
            .Create<StatusEffectApplyXOnKill>(Name)
            .WithText("Apply <{a}>{0} to ally in front")
            .WithStackable(true)
            .WithCanBeBoosted(true)
            .SubscribeToAfterAllBuildEvent<StatusEffectApplyXOnKill>(status =>
            {
                status.effectToApply = Absent.GetStatus(Calm.Name);
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.AllyInFrontOf;

                status.textInsert = Absent.KeywordTag(Keywords.Calm.Name);
            });
    }
}