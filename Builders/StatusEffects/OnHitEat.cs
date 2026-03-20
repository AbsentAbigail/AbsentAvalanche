#region

using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.StatusEffectImplementations;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Builders.StatusEffects;

[UsedImplicitly]
public class OnHitEat : IStatusBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Absent.Instance)
            .Create<StatusOnHitEat>(Name)
            .WithText("Eat and <keyword=absorb> targets with less <keyword=health> than my <keyword=attack>")
            .WithStackable(true)
            .WithCanBeBoosted(false)
            .SubscribeToAfterAllBuildEvent<StatusOnHitEat>(status =>
            {
                status.effectToApply = Absent.GetStatus(InstantEat.Name);
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.Target;
            });
    }
}