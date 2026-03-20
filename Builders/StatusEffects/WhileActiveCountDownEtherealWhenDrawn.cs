#region

using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Helpers;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Builders.StatusEffects;

[UsedImplicitly]
public class WhileActiveCountDownEtherealWhenDrawn : IStatusBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Absent.Instance)
            .Create<StatusEffectWhileActiveX>(Name)
            .WithText(
                $"While active, cards count down {Absent.KeywordTag(Keywords.Ethereal.Name)} by <{{a}}> when drawn")
            .WithStackable(true)
            .WithCanBeBoosted(true)
            .SubscribeToAfterAllBuildEvent<StatusEffectWhileActiveX>(status =>
            {
                status.effectToApply = Absent.GetStatus(CountDownEtherealWhenDrawn.Name);
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.Hand;

                status.applyConstraints = [TargetConstraintHelper.HasStatus(Ethereal.Name)];
            });
    }
}