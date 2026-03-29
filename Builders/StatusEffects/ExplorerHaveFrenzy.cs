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
public class ExplorerHaveFrenzy : IStatusBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Absent.Instance)
            .Create<StatusEffectExplorer>(Name)
            .WithText($"{Absent.KeywordTag(Explorer.Name)}: Have x{{a}}<keyword=frenzy>")
            .WithStackable(true)
            .WithCanBeBoosted(false)
            .SubscribeToAfterAllBuildEvent<StatusEffectExplorer>(status =>
            {
                status.evolutions = 
                [
                    ["CatcittenExplorer", "CatcittenFrenzy"],
                    ["CatcittenExplorerLeader", "CatcittenFrenzy"],
                ];
            });
    }
}