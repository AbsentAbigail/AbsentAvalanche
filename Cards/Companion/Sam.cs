using AbsentAvalanche.StatusEffects;
using AbsentUtilities;

namespace AbsentAvalanche.Cards.Companion;

public class Sam() : AbstractCompanion(Name, "Sam", 6, 2, 4,
    subscribe: card =>
    {
        card.startWithEffects =
        [
            AbsentUtils.SStack(WhenAllyAheadGainsStatusApplyItToAllies.Name)
        ];
    })
{
    public const string Name = "Sam";
    public override string FlavourText => "Old dog got tales to tell";
}