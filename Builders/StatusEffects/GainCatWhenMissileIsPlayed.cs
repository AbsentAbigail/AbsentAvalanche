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
public class GainCatWhenMissileIsPlayed : IStatusBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Absent.Instance)
            .Create<StatusEffectApplyXWhenCertainCardPlayed>(Name)
            .WithText($"Gain <{{a}}><keyword={Absent.PrefixGuid(Keywords.Cat.Name)}> when a {{0}} is played")
            .WithStackable(true)
            .WithCanBeBoosted(true)
            .SubscribeToAfterAllBuildEvent<StatusEffectApplyXWhenCertainCardPlayed>(status =>
            {
                status.effectToApply = Absent.GetStatus(Cat.Name);
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.Self;

                status.allowedCards = [Absent.GetCard(Missile.Name)];
                status.textInsert = Absent.CardTag(Missile.Name);
                status.doPing = false;
            });
    }
}