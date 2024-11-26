using AbsentAvalanche.Cards.Items;
using AbsentAvalanche.StatusEffects.Implementations;
using AbsentUtilities;

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
    public const string Name = "GainCatWhenItemIsPlayed";
}