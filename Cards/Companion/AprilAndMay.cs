using AbsentAvalanche.StatusEffects;
using AbsentUtilities;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;

namespace AbsentAvalanche.Cards.Companion;

public class AprilAndMay() : AbstractCompanion(Name, "Sheep and Dino", 8, 0, 3,
    pools: Pools.None,
    subscribe: card =>
    {
        card.startWithEffects =
        [
            AbsentUtils.SStack(DreamTeam.NameWhenDeployed(April.Name, May.Name))
        ];
        card.charmSlots *= 2;
    })
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public override string FlavourText => "Chaotic yet cuddly friends";

    public override CardDataBuilder Builder()
    {
        return base.Builder().WithText("Share Mays bonus health");
    }
}