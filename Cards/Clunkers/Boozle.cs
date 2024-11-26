using AbsentUtilities;

namespace AbsentAvalanche.Cards.Clunkers;

public class Boozle() : AbstractClunker(
    Name, "Boozle",
    3,
    subscribe: card =>
    {
        card.startWithEffects =
        [
            ..card.startWithEffects,
            AbsentUtils.SStack("When Hit Apply Shell To Self", 3)
        ];
    })
{
    public const string Name = "Boozle";
    public override string FlavourText => "A home for a friend";
}