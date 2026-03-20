#region

using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Builders.Traits;
using AbsentAvalanche.Helpers;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Builders.Cards.Companions;

[UsedImplicitly]
public class BigBroWombat : ICardBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateUnit(Name, "Big Bro Wombat")
            .SetStats(7, 2, 3)
            .SetSprites(
                Absent.GetSprite("BigBroWombat"),
                Absent.GetSprite("BigBroWombatBG"))
            .WithPools(CardPools.GeneralUnits)
            .DropsBling(4)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.traits =
                [
                    Absent.TStack(WombatParty.Name),
                    Absent.TStack("Barrage"),
                ];
                card.greetMessages =
                [
                    "*wombat noises*"
                ];
            });
    }
}