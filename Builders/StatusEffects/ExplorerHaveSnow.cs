#region

using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Builders.Keywords;
using AbsentAvalanche.StatusEffectImplementations;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Builders.StatusEffects;

[UsedImplicitly]
public class ExplorerHaveSnow : IStatusBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Absent.Instance)
            .Create<StatusEffectExplorer>(Name)
            .WithText($"{Absent.KeywordTag(Explorer.Name)}: Have {{a}}<keyword=snow>")
            .WithStackable(true)
            .WithCanBeBoosted(false)
            .SubscribeToAfterAllBuildEvent<StatusEffectExplorer>(status =>
            {
                status.evolutions = 
                [
                    ["CatcittenExplorer", "CatcittenSnow"],
                    ["CatcittenExplorerLeader", "CatcittenSnow"],
                ];
            });
    }
}