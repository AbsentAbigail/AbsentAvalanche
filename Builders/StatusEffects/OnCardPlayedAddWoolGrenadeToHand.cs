#region

using AbsentAvalanche.Builders.Cards.Items;
using AbsentAvalanche.Builders.Interfaces;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Builders.StatusEffects;

[UsedImplicitly]
public class OnCardPlayedAddWoolGrenadeToHand : IStatusBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return Absent.StatusCopy("On Card Played Add Junk To Hand", Name)
            .WithText("Add <{a}> {0} to hand")
            .SubscribeToAfterAllBuildEvent<StatusEffectApplyX>(status =>
            {
                status.effectToApply = Absent.GetStatus(InstantSummonWoolGrenadeInHand.Name);
                status.textInsert = Absent.CardTag(WoolGrenade.Name);
            });
    }
}