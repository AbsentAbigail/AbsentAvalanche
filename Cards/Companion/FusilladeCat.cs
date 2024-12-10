using AbsentAvalanche.StatusEffects;
using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.Cards.Companion;

internal class FusilladeCat() : AbstractCompanion(
    Name, "Fusillade Cat",
    9, 0, 3,
    Pools.None,
    card =>
    {
        card.startWithEffects =
        [
            AbsentUtils.SStack(Cat.Name),
            AbsentUtils.SStack("Weakness"),
            AbsentUtils.SStack(WhenEnemyIsHitByItemApplyWeaknessToThem.Name),
            AbsentUtils.SStack(GainCatWhenItemIsPlayed.Name),
            AbsentUtils.SStack(OnCardPlayedAddMissileToHand.Name, 2)
        ];
    })
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public override string FlavourText => "A cat with an explosive personality";
}