#region

using AbsentAvalanche.Builders.Interfaces;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Builders.Traits;

[UsedImplicitly]
public class Pilot : ITraitBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<TraitData, TraitDataBuilder> Builder()
    {
        return new TraitDataBuilder(Absent.Instance)
            .Create(Name)
            .SubscribeToAfterAllBuildEvent(trait => { trait.keyword = Absent.GetKeyword(Keywords.Pilot.Name); });
    }
}