#region

using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.StatusEffectImplementations;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Builders.StatusEffects;

[UsedImplicitly]
public class OnBattleWonDecreaseCounter : IStatusBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Absent.Instance)
            .Create<StatusEffectApplyXOnWin>(Name)
            .WithText("When you win a fight, reduce own <keyword=counter> by <{a}>")
            .WithStackable(true)
            .WithCanBeBoosted(true)
            .SubscribeToAfterAllBuildEvent<StatusEffectApplyXOnWin>(status =>
            {
                status.isBoss = false;
                
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.Self;
                status.effectToApply = Absent.GetStatus(InstantPermanentlyDecreaseCounter.Name);
            });
    }
}