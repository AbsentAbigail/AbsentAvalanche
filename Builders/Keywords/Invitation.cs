using AbsentAvalanche.Builders.Interfaces;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

namespace AbsentAvalanche.Builders.Keywords;

[UsedImplicitly]
public class Invitation : IKeywordBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name.ToLower();

    public DataFileBuilder<KeywordData, KeywordDataBuilder> Builder()
    {
        return new KeywordDataBuilder(Absent.Instance)
            .Create(Name)
            .WithTitle("Invitation")
            .WithTitleColour(KeywordColours.Orange)
            .WithShowName(true)
            .WithDescription("""
                             Item addressed to a random enemy in the next battle
                             Use it to make that enemy flee
                             """)
            .WithBodyColour(KeywordColours.White)
            .WithNoteColour(KeywordColours.Gray);
    }
}