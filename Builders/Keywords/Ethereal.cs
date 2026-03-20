#region

using AbsentAvalanche.Builders.Interfaces;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using UnityEngine;

#endregion

namespace AbsentAvalanche.Builders.Keywords;

[UsedImplicitly]
public class Ethereal : IKeywordBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name.ToLower();

    public DataFileBuilder<KeywordData, KeywordDataBuilder> Builder()
    {
        return new KeywordDataBuilder(Absent.Instance)
            .Create(Name)
            .WithTitle("Ethereal")
            .WithTitleColour(KeywordColours.EtherealColor)
            .WithDescription(
                "Destroy self when <sprite name=ethereal> expires|Counts down every turn while in hand")
            .WithBodyColour(KeywordColours.White)
            .WithNoteColour(KeywordColours.EtherealColor)
            .WithPanelColour(Color.black);
    }
}