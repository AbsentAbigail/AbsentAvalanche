#region

using AbsentAvalanche.Builders.Cards.Items;
using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Builders.Keywords;
using AbsentAvalanche.Helpers;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

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
            .AddToFlavours(VengefulAxe.Name);
    }
}