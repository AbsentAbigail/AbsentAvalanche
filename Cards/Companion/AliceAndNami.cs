using AbsentAvalanche.StatusEffects;
using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.Cards.Companion;

public class AliceAndNami() : AbstractCompanion(Name, "The coziest pacas", 10, 5, 4,
    pools: Pools.None,
    subscribe: card =>
    {
        card.startWithEffects =
        [
            AbsentUtils.SStack(DreamTeam.NameWhenDeployed(Alice.Name, Nami.Name)),
            AbsentUtils.SStack(WhenDeployedApplyRandomBuffToAllAllies.Name)
        ];
        card.charmSlots *= 2;
    })
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public override string FlavourText => "When you are this cozy, no problem can stand in your way";
}