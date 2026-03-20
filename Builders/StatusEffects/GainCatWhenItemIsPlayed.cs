#region

using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.StatusEffectImplementations;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Builders.StatusEffects;

[UsedImplicitly]
public class GainCatWhenItemIsPlayed : IStatusBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Absent.Instance)
            .Create<StatusEffectApplyXWhenCertainCardPlayed>(Name)
            .WithText("Gain <{a}>{0} when an <Item> is played")
            .WithStackable(true)
            .WithCanBeBoosted(true)
            .SubscribeToAfterAllBuildEvent<StatusEffectApplyXWhenCertainCardPlayed>(status =>
            {
                status.effectToApply = Absent.GetStatus(Cat.Name);
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.Self;

                status.allowedCardType = Absent.GetCardType("Item");
                status.doPing = false;
                status.textInsert = Absent.KeywordTag(Keywords.Cat.Name);
            });
    }
}