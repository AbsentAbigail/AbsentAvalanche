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
public class InstantRandomBuff : IStatusBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Absent.Instance)
            .Create<StatusEffectInstantApplyRandom>(Name)
            .WithText(Absent.KeywordTag(RainbowFluff.Name))
            .WithStackable(true)
            .WithCanBeBoosted(false)
            .SubscribeToAfterAllBuildEvent<StatusEffectInstantApplyRandom>(status =>
            {
                status.applyFormatKey = Absent.GetStatus("Shroom").applyFormatKey;

                status.possibleEffects =
                [
                    Absent.GetStatus(Cat.Name),
                    Absent.GetStatus("Block"),
                    Absent.GetStatus("MultiHit"),
                    Absent.GetStatus("Shell"),
                    Absent.GetStatus("Spice"),
                    Absent.GetStatus("Teeth"),
                    Absent.GetStatus("Reduce Counter"),
                    Absent.GetStatus("Reduce Max Counter")
                ];
            });
    }
}