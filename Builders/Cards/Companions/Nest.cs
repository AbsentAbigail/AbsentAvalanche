#region

using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Helpers;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Builders.Cards.Companions;

[UsedImplicitly]
public class Nest : ICardBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        var sprite = Absent.GetSprite("Nest");
        sprite.name = "Nothing";
        return new CardDataBuilder(Absent.Instance)
            .CreateUnit(Name, "Obscured Nest")
            .SetStats(2)
            .SetSprites(
                sprite,
                Absent.GetSprite("PipBG"))
            .DropsBling(4)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.AddToPets();
            })
            .WithText("Turn into a <Pip Egg> or <Cuddle Pip Egg> on pickup");
    }
}