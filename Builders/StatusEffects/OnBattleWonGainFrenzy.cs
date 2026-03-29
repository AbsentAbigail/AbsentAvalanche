#region

using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.StatusEffectImplementations;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Builders.StatusEffects;

[UsedImplicitly]
public class OnBattleWonGainFrenzy : IStatusBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Absent.Instance)
            .Create<StatusEffectApplyXOnWin>(Name)
            .WithText("When you win a fight, gain <x{a}><keyword=frenzy>")
            .WithStackable(true)
            .WithCanBeBoosted(true)
            .SubscribeToAfterAllBuildEvent<StatusEffectApplyXOnWin>(status =>
            {
                status.isBoss = false;
                
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.Self;
                status.effectToApply = Absent.GetStatus(InstantPermanentlyGainFrenzy.Name);
            });
    }
}