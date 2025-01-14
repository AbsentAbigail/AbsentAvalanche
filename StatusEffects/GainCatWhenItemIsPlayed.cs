using AbsentAvalanche.Cards.Items;
using AbsentAvalanche.StatusEffects.Implementations;
using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.StatusEffects;

public class GainCatWhenItemIsPlayed() : AbstractApplyXStatus<StatusEffectApplyXWhenCertainCardPlayed>(
    Name, $"Gain <{{a}}>{Keywords.Cat.Tag} when an <Item> is played",
    canBoost: true,
    effectToApply: Cat.Name,
    subscribe: status =>
    {
        status.allowedCardType = AbsentUtils.TryGet<CardType>("Item");
        status.doPing = false;
    })
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}