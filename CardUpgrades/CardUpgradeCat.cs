using AbsentAvalanche.StatusEffects;
using AbsentUtilities;
using HarmonyLib;
using Cat = AbsentAvalanche.Keywords.Cat;

namespace AbsentAvalanche.CardUpgrades;

internal class CardUpgradeCat() : AbstractCardUpgrade(
    Name, "Kids Drawing",
    $"<-2><keyword=attack>\nTrigger: Gain <1>{Cat.Tag}",
    subscribe: charm =>
    {
        charm.damage = -2;
        charm.effects =
        [
            AbsentUtils.SStack(OnCardPlayedGainCat.Name)
        ];
        charm.targetConstraints =
        [
            TargetConstraintHelper.AttackMoreThan(1),
            TargetConstraintHelper.DoesTrigger(),
            TargetConstraintHelper.General<TargetConstraintPlayOnSlot>(
                "Does Not Play On Slot",
                tc => tc.slot = true,
                not: true
            ),
            TargetConstraintHelper.General<TargetConstraintPlayOnSlot>(
                "Plays On Board",
                tc => tc.board = true
            )
        ];
    })
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}