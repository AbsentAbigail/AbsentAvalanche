#region

using AbsentAvalanche.Builders.Interfaces;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Builders.Keywords;

[UsedImplicitly]
public class Scavenge : IKeywordBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name.ToLower();

    public DataFileBuilder<KeywordData, KeywordDataBuilder> Builder()
    {
        return new KeywordDataBuilder(Absent.Instance)
            .Create(Name)
            .WithTitle("Scavenge")
            .WithTitleColour(KeywordColours.Scavenge)
            .WithShowName(true)
            .WithDescription("On <boss> or <miniboss> kill, permanently gain a random charm")
            .WithBodyColour(KeywordColours.White)
            .WithNoteColour(KeywordColours.Gray)
            .WithCanStack(true);
    }
}