using AbsentAvalanche.StatusEffects;
using AbsentUtilities;

namespace AbsentAvalanche.Cards.Items;

internal class Imagination() : AbstractItem(
    Name, "Imagination",
    subscribe: card =>
    {
        card.startWithEffects =
        [
            AbsentUtils.SStack(OnCardPlayedTutorRandomCardZoomlin.Name)
        ];
    })
{
    public const string Name = "Imagination";
    public override string FlavourText => "Let your creativity be your limit";
}