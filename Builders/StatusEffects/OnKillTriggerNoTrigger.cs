#region

using AbsentAvalanche.Builders.Interfaces;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Builders.StatusEffects;

[UsedImplicitly]
public class OnKillTriggerNoTrigger : IStatusBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Absent.Instance)
            .Create<StatusEffectApplyXOnKill>(Name)
            .WithStackable(true)
            .WithCanBeBoosted(false)
            .SubscribeToAfterAllBuildEvent<StatusEffectApplyXOnKill>(status =>
            {
                status.effectToApply = Absent.GetStatus(TriggerNoTrigger.Name);
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.Self;
                status.textInsert = "<{a}><keyword=health>";
            });
    }
}