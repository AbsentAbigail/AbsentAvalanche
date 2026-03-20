#region

using AbsentAvalanche.Builders.Interfaces;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Builders.Keywords;

[UsedImplicitly]
public class Rest : IKeywordBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name.ToLower();

    public DataFileBuilder<KeywordData, KeywordDataBuilder> Builder()
    {
        return new KeywordDataBuilder(Absent.Instance)
            .Create(Name)
            .WithTitle("Rest")
            .WithTitleColour(KeywordColours.Rest)
            .WithShowName(true)
            .WithDescription(
                $"Increase {Absent.KeywordTag(Ethereal.Name)} to match <Rest> when played or discarded")
            .WithBodyColour(KeywordColours.White)
            .WithNoteColour(KeywordColours.Gray)
            .WithCanStack(true);
    }
}