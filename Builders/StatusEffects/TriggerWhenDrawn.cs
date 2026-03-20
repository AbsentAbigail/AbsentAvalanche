#region

using AbsentAvalanche.Builders.Interfaces;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Builders.StatusEffects;

[UsedImplicitly]
public class TriggerWhenDrawn : IStatusBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Absent.Instance)
            .Create<StatusEffectApplyXWhenDrawn>(Name)
            .WithText("Trigger when drawn")
            .WithStackable(true)
            .WithCanBeBoosted(false)
            .SubscribeToAfterAllBuildEvent<StatusEffectApplyXWhenDrawn>(status =>
            {
                status.effectToApply = Absent.GetStatus("Trigger Against & Reduce Uses");
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.RandomEnemy;

                status.isReaction = true;
                status.descColorHex = "F99C61";
            });
    }
}