using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Builders.Keywords;
using AbsentAvalanche.Helpers;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

namespace AbsentAvalanche.Builders.Cards.Companions;

[UsedImplicitly]
public class EmptySeat : ICardBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateUnit(Name, "Empty Seat")
            .SetStats(1, null, 2)
            .SetSprites(
                Absent.GetSprite("EmptySeat"),
                Absent.GetSprite("EmptySeatBG"))
            .WithFlavour("But no one showed up...")
            .DropsBling(4)
            .WithText($"<color=#{KeywordColours.Gray.ToHexRGB()}>But no one showed up...");
    }
}