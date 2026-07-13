using AbsentAvalanche.Builders.Interfaces;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

namespace AbsentAvalanche.Builders.Keywords;

[UsedImplicitly]
public class Metronome : IKeywordBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name.ToLower();

    public DataFileBuilder<KeywordData, KeywordDataBuilder> Builder()
    {
        return new KeywordDataBuilder(Absent.Instance)
            .Create(Name)
            .WithTitle("Metronome")
            .WithTitleColour(KeywordColours.Orange)
            .WithShowName(true)
            .WithDescription("""
                             Randomly improve this card before triggering|"It could be anything, it could even be good"
                             """)
            .WithBodyColour(KeywordColours.White)
            .WithNoteColour(KeywordColours.Gray);
    }
}