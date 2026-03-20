#region

using AbsentAvalanche.Builders.Interfaces;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Builders.Keywords;

[UsedImplicitly]
public class Calm : IKeywordBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name.ToLower();

    public DataFileBuilder<KeywordData, KeywordDataBuilder> Builder()
    {
        return new KeywordDataBuilder(Absent.Instance)
            .Create(Name)
            .WithTitle("Calm")
            .WithTitleColour(KeywordColours.Blue)
            .WithDescription(
                "Reduce max <keyword=counter> for every three <sprite name=calm>|Halves when damage is taken")
            .WithBodyColour(KeywordColours.Pink)
            .WithNoteColour(KeywordColours.White);
    }
}