using AbsentAvalanche.Cards.Items;
using AbsentUtilities;

namespace AbsentAvalanche.StatusEffects;

public class WhileActiveMissilesHaveCat() : AbstractApplyXStatus<StatusEffectWhileActiveX>(
    Name, "While Active, {0} have <{a}>" + Keywords.Cat.Tag,
    canBoost: true,
    effectToApply: Cat.Name,
    applyToFlags: StatusEffectApplyX.ApplyToFlags.Hand,
    subscribe: status =>
    {
        var card = AbsentUtils.GetCard(Missile.Name);
        status.textInsert = CardHelper.CardTag(Missile.Name);
        status.applyConstraints =
        [
            TargetConstraintHelper.General<TargetConstraintIsSpecificCard>("Is Missile", tc => tc.allowedCards = [card])
        ];
    })
{
    public const string Name = "While Active Missiles Have Cat";
}