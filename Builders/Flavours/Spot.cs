#region

using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Builders.Keywords;
using AbsentAvalanche.Helpers;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Builders.Flavours;

[UsedImplicitly]
public class Spot : IKeywordBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name.ToLower() + "_flavour";

    public DataFileBuilder<KeywordData, KeywordDataBuilder> Builder()
    {
        return new KeywordDataBuilder(Absent.Instance)
            .Create(Name)
            .WithTitle("")
            .WithTitleColour(KeywordColours.Orange)
            .WithShowName(true)
            .WithDescription(
                "He/Him|" + Cards.Companions.Spot.Flavour + "\n(Sprite by Nina)")
            .WithBodyColour(KeywordColours.Flavour)
            .WithNoteColour(KeywordColours.Gray)
            .AddToFlavours(Cards.Companions.Spot.Name);
    }
}