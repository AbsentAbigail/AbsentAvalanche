#region

using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Helpers;
using AbsentAvalanche.StatusEffectImplementations;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using WildfrostHopeMod.VFX;

#endregion

namespace AbsentAvalanche.Builders.StatusEffects;

[UsedImplicitly]
public class Cat : IStatusBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Absent.Instance)
            .Create<StatusEffectCat>(Name)
            .WithStackable(true)
            .WithCanBeBoosted(false)
            .WithTextInsert("{a}")
            .SubscribeToAfterAllBuildEvent<StatusEffectCat>(status =>
            {
                status.applyFormatKey = Absent.GetStatus("Shroom").applyFormatKey;
                status.doesDamage = true;
                status.dealDamage = true;
                status.countsAsHit = true;
                status.applyEqualAmount = true;

                status.targetConstraints =
                [
                    TargetConstraintHelper.DoesTrigger(),
                    TargetConstraintHelper.General<TargetConstraintDoesDamage>("Does Damage")
                ];
            })
            .Subscribe_WithStatusIcon("cat");
    }
}