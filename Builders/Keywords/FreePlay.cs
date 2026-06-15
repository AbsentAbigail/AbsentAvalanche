using AbsentAvalanche.Builders.Interfaces;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

namespace AbsentAvalanche.Builders.Keywords;

[UsedImplicitly]
public class FreePlay : IKeywordBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name.ToLower();

    public DataFileBuilder<KeywordData, KeywordDataBuilder> Builder()
    {
        return new KeywordDataBuilder(Absent.Instance)
            .Create(Name)
            .WithTitle("Free Play")
            .WithTitleColour(KeywordColours.Blue)
            .WithDescription(
                "Cards you play do not end your turn|Then counts down\n(Sprite by Pelli)")
            .WithBodyColour(KeywordColours.White)
            .WithNoteColour(KeywordColours.Blue);
    }
}