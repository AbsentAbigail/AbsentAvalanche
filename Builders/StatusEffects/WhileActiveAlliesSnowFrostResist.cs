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
public class WhileActiveAlliesSnowFrostResist : IStatusBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Absent.Instance)
            .Create<StatusEffectResistStatus>(Name)
            .WithText(
                $"While active, allies {Absent.KeywordTag(Resist.Name)} <sprite name=snow> and <sprite name=frost>")
            .WithStackable(true)
            .WithCanBeBoosted(false)
            .SubscribeToAfterAllBuildEvent<StatusEffectResistStatus>(status =>
            {
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.Allies;
                
                status.resistTypes =
                [
                    Absent.GetStatus("Frost").type,
                    Absent.GetStatus("Snow").type
                ];
            });
    }
}