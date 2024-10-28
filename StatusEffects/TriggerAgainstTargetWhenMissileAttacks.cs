using AbsentAvalanche.Cards.Items;
using AbsentAvalanche.StatusEffects.Implementations;
using AbsentUtilities;

namespace AbsentAvalanche.StatusEffects;

internal class TriggerAgainstTargetWhenMissileAttacks() : AbstractStatus<StatusEffectTriggerWhenCardIsPlayed>(
    Name, "Trigger against the target when a {0} is played",
    subscribe: status =>
    {
        status.isReaction = true;
        status.descColorHex = "F99C61";
        status.whenCardsPlayed = [AbsentUtils.GetCard(Missile.Name)];
        status.textInsert = AbstractCard.CardTag(Missile.Name);
    })
{
    public const string Name = "Trigger Against Target When Missile Attacks";
}