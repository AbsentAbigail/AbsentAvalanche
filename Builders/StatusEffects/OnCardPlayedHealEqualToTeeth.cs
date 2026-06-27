using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Helpers;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

namespace AbsentAvalanche.Builders.StatusEffects;

[UsedImplicitly]
public class OnCardPlayedHealEqualToTeeth : IStatusBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Absent.Instance)
            .Create<StatusEffectApplyXOnCardPlayed>(Name)
            .WithText("Restore own <keyword=health> equal to <keyword=teeth>")
            .WithStackable(false)
            .WithCanBeBoosted(false)
            .SubscribeToAfterAllBuildEvent<StatusEffectApplyXOnCardPlayed>(status =>
            {
                status.effectToApply = Absent.GetStatus("Heal (No Ping)");
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.Self;
                status.scriptableAmount = new Script<ScriptableCurrentStatus>("Current teeth",
                    script => script.statusType = Absent .GetStatus("Teeth").type);
            });
    }
}