#region

using AbsentAvalanche.Builders.Interfaces;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Builders.Keywords;

[UsedImplicitly]
public class Cascade : IKeywordBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name.ToLower();

    public DataFileBuilder<KeywordData, KeywordDataBuilder> Builder()
    {
        return new KeywordDataBuilder(Absent.Instance)
            .Create(Name)
            .WithTitle("Cascade")
            .WithTitleColour(KeywordColours.Orange)
            .WithShowName(true)
            .WithDescription(
                "Effect cascades to allies behind, doubling each time")
            .WithBodyColour(KeywordColours.White)
            .WithNoteColour(KeywordColours.Gray);
    }
}