#region

using AbsentAvalanche.Builders.Interfaces;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Builders.Keywords;

[UsedImplicitly]
public class Equip : IKeywordBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name.ToLower();

    public DataFileBuilder<KeywordData, KeywordDataBuilder> Builder()
    {
        return new KeywordDataBuilder(Absent.Instance)
            .Create(Name)
            .WithTitle("Equip")
            .WithTitleColour(KeywordColours.EquipColor)
            .WithShowName(true)
            .WithDescription("""
                             Equip to a unit on board
                             It gains the equipments stats and effects|When it dies, return the equipment to hand
                             (Sprite by Pelli)
                             """)
            .WithBodyColour(KeywordColours.White)
            .WithNoteColour(KeywordColours.EquipColor);
    }
}