using AbsentUtilities;
using HarmonyLib;

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
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public override string FlavourText => "A home for a friend";
}