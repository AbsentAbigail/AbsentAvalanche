using AbsentAvalanche.Keywords;
using AbsentAvalanche.Patches;
using AbsentAvalanche.StatusEffects;
using AbsentUtilities;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using Cat = AbsentAvalanche.StatusEffects.Cat;

namespace AbsentAvalanche.Cards.Companion;

internal class Catcus() : AbstractCompanion(
    Name, "Catcus",
    7, 0, 4,
    Pools.None,
    card =>
    {
        CardPatches.Flavours = [
            ..CardPatches.Flavours,
            
        ];
        card.AddToPets();
        card.startWithEffects =
        [
            AbsentUtils.SStack("Teeth"),
            AbsentUtils.SStack(Cat.Name),
            AbsentUtils.SStack("MultiHit"),
            AbsentUtils.SStack(OnKillGainCat.Name)
        ];
        card.createScripts =
        [
            LeaderHelper.GiveUpgrade()
        ];
    })
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public override string FlavourText => "Sun loving, spiky feline with an adventurous Spirit! :3";
    protected override string BloodProfile => "Blood Profile Pink Wisp";
    
    public override CardDataBuilder Builder()
    {
        return base.Builder()
            .WithText(Royal.Tag)
            .WithCardType("Leader");
    }
}