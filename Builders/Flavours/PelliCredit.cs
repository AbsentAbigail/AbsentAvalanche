using AbsentAvalanche.Builders.Cards.Clunkers;
using AbsentAvalanche.Builders.Cards.Companions;
using AbsentAvalanche.Builders.Cards.Items;
using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Builders.Keywords;
using AbsentAvalanche.Helpers;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

namespace AbsentAvalanche.Builders.Flavours;

[UsedImplicitly]
public class PelliCredit : IKeywordBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name.ToLower() + "_flavour";

    public DataFileBuilder<KeywordData, KeywordDataBuilder> Builder()
    {
        return new KeywordDataBuilder(Absent.Instance)
            .Create(Name)
            .WithTitle("")
            .WithTitleColour(KeywordColours.Orange)
            .WithShowName(true)
            .WithDescription(" |(Sprites by Pelli)")
            .WithBodyColour(KeywordColours.Flavour)
            .WithNoteColour(KeywordColours.Gray)
            .AddToFlavours(BiglooCosplay.Name)
            .AddToFlavours(BunHum.Name)
            .AddToFlavours(Earrings.Name)
            .AddToFlavours(HogCosplay.Name)
            .AddToFlavours(MawJawCosplay.Name)
            .AddToFlavours(MorningStar.Name)
            .AddToFlavours(RazorPlush.Name)
            .AddToFlavours(RingerCosplay.Name)
            .AddToFlavours(VengefulAxe.Name)
            .AddToFlavours(Airliner.Name)
            .AddToFlavours(Airship.Name)
            .AddToFlavours(FighterJet.Name)
            .AddToFlavours(Paperplane.Name)
            .AddToFlavours(PrivateJet.Name)
            .AddToFlavours(RescueHelicopter.Name)
            .AddToFlavours(Seaplane.Name)
            .AddToFlavours(EmptySeat.Name)
            .AddToFlavours(ButterflyKnife.Name)
            .AddToFlavours(Chakram.Name)
            .AddToFlavours(Coolant.Name)
            .AddToFlavours(JumpStart.Name)
            .AddToFlavours(MarshallingLights.Name)
            .AddToFlavours(StoppedClock.Name)
            .AddToFlavours(Uppies.Name)
            ;
    }
}