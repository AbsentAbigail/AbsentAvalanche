#region

using AbsentAvalanche.Builders.Interfaces;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Builders.StatusEffects;

[UsedImplicitly]
public class WhenEnemyIsHitByItemApplyWeaknessToThem : IStatusBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return Absent.StatusCopy("When Enemy Is Hit By Item Apply Demonize To Them", Name)
            .WithText($"When an enemy is hit with an <Item>, apply <{{a}}>{Absent.VanillaKeywordTag("weakness")} to them")
            .SubscribeToAfterAllBuildEvent<StatusEffectApplyXWhenUnitIsHit>(status =>
            {
                status.effectToApply = Absent.GetStatus("Weakness");
            });
    }
}