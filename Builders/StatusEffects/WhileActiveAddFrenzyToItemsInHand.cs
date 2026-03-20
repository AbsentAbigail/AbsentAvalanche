#region

using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Helpers;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Builders.StatusEffects;

[UsedImplicitly]
public class WhileActiveAddFrenzyToItemsInHand : IStatusBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Absent.Instance)
            .Create<StatusEffectWhileActiveX>(Name)
            .WithText($"While Active, <Items> gain <x{{a}}>{Absent.VanillaKeywordTag("frenzy")}")
            .WithStackable(true)
            .WithCanBeBoosted(true)
            .SubscribeToAfterAllBuildEvent<StatusEffectWhileActiveX>(status =>
            {
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.Hand;
                status.effectToApply = Absent.GetStatus("MultiHit");

                status.applyConstraints =
                [
                    TargetConstraintHelper.General<TargetConstraintIsItem>("Is Item")
                ];
            });
    }
}