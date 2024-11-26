using AbsentAvalanche.StatusEffects;
using AbsentUtilities;

namespace AbsentAvalanche.Cards.Companion;

public class Bam() : AbstractCompanion(Name, "Bam", 3, 1, 8,
    subscribe: card =>
    {
        card.startWithEffects =
        [
            AbsentUtils.SStack(WhenAllyHitGainFrenzy.Name)
        ];
    })
{
    public const string Name = "Bam";
    public override string FlavourText => "A friend for a home";
}