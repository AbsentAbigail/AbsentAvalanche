#region

using AbsentAvalanche.Builders.Interfaces;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using UnityEngine;

#endregion

namespace AbsentAvalanche.Builders.Keywords;

[UsedImplicitly]
public class Abduct : IKeywordBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name.ToLower();

    public DataFileBuilder<KeywordData, KeywordDataBuilder> Builder()
    {
        return new KeywordDataBuilder(Absent.Instance)
            .Create(Name)
            .WithTitle("Abduct")
            .WithTitleColour(KeywordColours.DarkPurple)
            .WithDescription(
                "Untargetable and <keyword=snow>'d for one turn")
            .WithBodyColour(KeywordColours.LightPurple)
            .WithNoteColour(KeywordColours.White)
            .WithPanelColour(Color.black);
    }
}