#region

using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.StatusEffectImplementations;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Builders.StatusEffects;

[UsedImplicitly]
public class WhileInHandApplyOverburnToRandomEnemy : IStatusBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Absent.Instance)
            .Create<StatusEffectApplyXEveryTurnInHand>(Name)
            .WithText($"While in hand, every turn apply <{{a}}>{Absent.VanillaKeywordTag("overload")} to a random enemy")
            .WithStackable(true)
            .WithCanBeBoosted(true)
            .SubscribeToAfterAllBuildEvent<StatusEffectApplyXEveryTurnInHand>(status =>
            {
                status.effectToApply = Absent.GetStatus("Overload");
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.RandomEnemy;
            });
    }
}