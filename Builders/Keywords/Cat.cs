#region

using AbsentAvalanche.Builders.Interfaces;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Builders.Keywords;

[UsedImplicitly]
public class Cat : IKeywordBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name.ToLower();

    public DataFileBuilder<KeywordData, KeywordDataBuilder> Builder()
    {
        return new KeywordDataBuilder(Absent.Instance)
            .Create(Name)
            .WithTitle("Cat")
            .WithTitleColour(KeywordColours.CatColor)
            .WithDescription(
                "Deal <1> damage additional times|Watch out for the claws")
            .WithBodyColour(KeywordColours.White)
            .WithNoteColour(KeywordColours.CatColor);
    }
}