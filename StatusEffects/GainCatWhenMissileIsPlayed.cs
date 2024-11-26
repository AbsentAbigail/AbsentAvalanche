using AbsentAvalanche.Cards.Items;
using AbsentAvalanche.StatusEffects.Implementations;
using AbsentUtilities;

namespace AbsentAvalanche.StatusEffects;

public class GainCatWhenMissileIsPlayed() : AbstractApplyXStatus<StatusEffectApplyXWhenCertainCardPlayed>(
    Name, $"Gain <{{a}}>{Keywords.Cat.Tag} when a {{0}} is played",
    canBoost: true,
    effectToApply: Cat.Name,
    subscribe: status =>
    {
        status.allowedCards = [AbsentUtils.GetCard(Missile.Name)];
        status.textInsert = AbstractCard.CardTag(Missile.Name);
        status.doPing = false;
    })
{
    public const string Name = "Gain Cat When Missile Is Played";
}