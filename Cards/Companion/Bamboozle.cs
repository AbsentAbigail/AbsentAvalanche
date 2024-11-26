using AbsentAvalanche.StatusEffects;
using AbsentUtilities;

namespace AbsentAvalanche.Cards.Companion;

public class Bamboozle() : AbstractCompanion(Name, "Bam and Boozle", 3, 1, 8,
    pools: Pools.None,
    subscribe: card =>
    {
        card.startWithEffects =
        [
            AbsentUtils.SStack("Scrap", 3),
            AbsentUtils.SStack(WhenDeployedSplitIntoBamAndBoozle.Name)
        ];
        card.charmSlots *= 2;
    })
{
    public const string Name = "Bamboozle";
    public override string FlavourText => "United forever";
}