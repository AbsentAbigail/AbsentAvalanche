#region

using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.StatusEffectImplementations;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Builders.StatusEffects;

[UsedImplicitly]
public class ProgressExplorerOverkill : IStatusBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Absent.Instance)
            .Create<StatusEffectProgressExplorer>(Name)
            .WithStackable(false)
            .WithCanBeBoosted(false)
            .SubscribeToAfterAllBuildEvent<StatusEffectProgressExplorer>(status =>
            {
                status.countDownName = Absent.GetStatus(ExplorerOverkill.Name).name;
            });
    }
}