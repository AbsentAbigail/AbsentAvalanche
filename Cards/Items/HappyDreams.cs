using AbsentAvalanche.StatusEffects;
using AbsentUtilities;

namespace AbsentAvalanche.Cards.Items;

internal class HappyDreams() : AbstractItem(
    Name, "Happy Dreams",
    subscribe: card =>
    {
        card.startWithEffects =
        [
            AbsentUtils.SStack(OnCardPlayedTutorDeckCopyConsumeZoomlin.Name)
        ];
    })
{
    public const string Name = "HappyDreams";
    public override string FlavourText => "Dreams are the fuel of the future";
}