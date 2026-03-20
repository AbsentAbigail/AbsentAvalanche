#region

using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Builders.StatusEffects;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Builders.Traits;

[UsedImplicitly]
public class Valor : ITraitBuilder
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
                    Absent.GetStatus(HitHighestAttack.Name)
                ];
                trait.keyword = Absent.GetKeyword(Keywords.Valor.Name);
            })
            .WithOverrides(
                Absent.GetTrait("Barrage"),
                Absent.GetTrait("Aimless"),
                Absent.GetTrait("Longshot")
            );
    }
}