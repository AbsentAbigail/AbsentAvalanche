using AbsentAvalanche.Keywords;
using AbsentAvalanche.StatusEffects;
using AbsentUtilities;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using Cat = AbsentAvalanche.StatusEffects.Cat;
using DreamTeam = AbsentAvalanche.StatusEffects.DreamTeam;

namespace AbsentAvalanche.Cards.Companion;

internal class Catci() : AbstractCompanion(
    Name, "Catci",
    10, 0, 4,
    pools: Pools.None,
    subscribe: card =>
    {
        card.startWithEffects =
        [
            AbsentUtils.SStack(Cat.Name, 2),
            AbsentUtils.SStack("MultiHit"),
            AbsentUtils.SStack(DreamTeam.NameWhenDeployed(Catcus.Name, Catcitten.Name))
        ];
        card.createScripts =
        [
            LeaderHelper.GiveUpgrade()
        ];
        card.charmSlots *= 2;
    })
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public override string FlavourText => "Prepare for trouble, and make it double";
    protected override string BloodProfile => "Blood Profile Pink Wisp";
    
    public override CardDataBuilder Builder()
    {
        return base.Builder()
            .WithText(Royal.Tag)
            .WithCardType("Leader");
    }
}