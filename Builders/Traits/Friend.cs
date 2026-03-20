#region

using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Builders.StatusEffects;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Builders.Traits;

[UsedImplicitly]
public class Friend : ITraitBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<TraitData, TraitDataBuilder> Builder()
    {
        return new TraitDataBuilder(Absent.Instance)
            .Create(Name)
            .SubscribeToAfterAllBuildEvent(trait =>
            {
                trait.effects =
                [
                    Absent.GetStatus(WhenAllyGainsNegativeStatusApplyToSelfInstead.Name),
                    Absent.GetStatus(WhenAnAllyGainsAPositiveStatusShareHalfToSelf.Name)
                ];
                trait.keyword = Absent.GetKeyword(Keywords.Friend.Name);
            });
    }
}