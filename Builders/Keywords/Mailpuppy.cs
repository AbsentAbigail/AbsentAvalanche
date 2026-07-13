using AbsentAvalanche.Builders.Interfaces;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

namespace AbsentAvalanche.Builders.Keywords;

[UsedImplicitly]
public class Mailpuppy : IKeywordBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name.ToLower();

    public DataFileBuilder<KeywordData, KeywordDataBuilder> Builder()
    {
        return new KeywordDataBuilder(Absent.Instance)
            .Create(Name)
            .WithTitle("Mailpuppy")
            .WithTitleColour(KeywordColours.Orange)
            .WithShowName(true)
            .WithDescription($"""
                             When selected, also gain an {Absent.KeywordTag("invitation")}
                             When delivered, permanently reduce <sprite name=counter> by <1> and gain another {Absent.KeywordTag("invitation")}
                             """)
            .WithBodyColour(KeywordColours.White)
            .WithNoteColour(KeywordColours.Gray);
    }
}