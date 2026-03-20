#region

using AbsentAvalanche.Builders.Cards.Items;
using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.StatusEffectImplementations;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Builders.StatusEffects;

[UsedImplicitly]
public class TriggerAgainstTargetWhenMissileAttacks : IStatusBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Absent.Instance)
            .Create<StatusEffectTriggerWhenCardIsPlayed>(Name)
            .WithText("Trigger against the target when a {0} is played")
            .WithStackable(true)
            .WithCanBeBoosted(false)
            .SubscribeToAfterAllBuildEvent<StatusEffectTriggerWhenCardIsPlayed>(status =>
            {
                status.isReaction = true;
                status.descColorHex = "F99C61";
                status.whenCardsPlayed = [Absent.GetCard(Missile.Name)];
                status.textInsert = Absent.CardTag(Missile.Name);
            });
    }
}