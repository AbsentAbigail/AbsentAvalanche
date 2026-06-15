using AbsentAvalanche.Builders.Interfaces;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

namespace AbsentAvalanche.Builders.Keywords;

[UsedImplicitly]
public class Flight : IKeywordBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name.ToLower();

    public DataFileBuilder<KeywordData, KeywordDataBuilder> Builder()
    {
        return new KeywordDataBuilder(Absent.Instance)
            .Create(Name)
            .WithTitle("Flight")
            .WithTitleColour(KeywordColours.Blue)
            .WithDescription(
                "Take half damage|Counts down when hit")
            .WithBodyColour(KeywordColours.White)
            .WithNoteColour(KeywordColours.Blue);
    }
}