using AbsentAvalanche.Builders.Cards.Items;
using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Builders.Keywords;
using AbsentAvalanche.Helpers;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

namespace AbsentAvalanche.Builders.Flavours;

[UsedImplicitly]
public class JiraCredit : IKeywordBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name.ToLower() + "_flavour";

    public DataFileBuilder<KeywordData, KeywordDataBuilder> Builder()
    {
        return new KeywordDataBuilder(Absent.Instance)
            .Create(Name)
            .WithTitle("")
            .WithTitleColour(KeywordColours.Orange)
            .WithShowName(true)
            .WithDescription(" |(Sprites by LadyJira)")
            .WithBodyColour(KeywordColours.Flavour)
            .WithNoteColour(KeywordColours.Gray)
            .AddToFlavours(Cannon.Name);
    }
}