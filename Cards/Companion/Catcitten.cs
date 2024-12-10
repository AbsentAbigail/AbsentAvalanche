using AbsentAvalanche.StatusEffects;
using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.Cards.Companion;

internal class Catcitten() : AbstractCompanion(
    Name, "Catcitten",
    3, 0, 5,
    subscribe: card =>
    {
        card.startWithEffects =
        [
            AbsentUtils.SStack(Cat.Name),
            AbsentUtils.SStack(WhenEnemyIsKilledApplyTeethToAttacker.Name),
            AbsentUtils.SStack(WhenEnemyIsKilledCountDownAttacker.Name),
        ];
        card.greetMessages =
        [
            "I got sepewated fwom my big sister, have you seen her? She's de coolest!"
        ];
    })
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public override string FlavourText => "Takes after their big sister, but still a bit shy!";
    protected override string BloodProfile => "Blood Profile Pink Wisp";
}