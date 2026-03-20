#region

using AbsentAvalanche.Builders.Interfaces;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Builders.StatusEffects;

[UsedImplicitly]
public class WhenEnemyIsKilledApplyTeethToAttacker : IStatusBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return Absent.StatusCopy("When Enemy Is Killed Apply Shell To Attacker", Name)
            .WithTextInsert($"<{{a}}>{Absent.VanillaKeywordTag("teeth")}")
            .SubscribeToAfterAllBuildEvent<StatusEffectApplyXWhenUnitIsKilled>(status =>
            {
                status.effectToApply = Absent.GetStatus("Teeth");
            });
    }
}