using AbsentAvalanche.StatusEffects;
using AbsentUtilities;
using Deadpan.Enums.Engine.Components.Modding;

namespace AbsentAvalanche.Cards.Companion;

internal class Kramig() : AbstractCompanion(
    Name, "Kramig",
    8, 3, 4,
    subscribe: card =>
    {
        card.startWithEffects = [AbsentUtils.SStack(Stress.Name, 2)];
        card.greetMessages =
        [
            "In the wild, an adult panda eats about 83 pounds of bamboo – every day! But this black and white softie doesn’t need any food, just a lot of love.",
            "Protects its friends"
        ];
    })
{
    public const string Name = "Kramig";
    public override string FlavourText => "Protects its friends";
}