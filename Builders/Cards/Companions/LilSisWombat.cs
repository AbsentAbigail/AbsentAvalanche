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
public class LilSisWombat : ICardBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateUnit(Name, "Lil Sis Wombat")
            .SetStats(3, 1, 1)
            .SetSprites(
                Absent.GetSprite("LilSisWombat"),
                Absent.GetSprite("LilSisWombatBG"))
            .WithPools(CardPools.GeneralUnits)
            .DropsBling(4)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.traits =
                [
                    Absent.TStack(WombatParty.Name),
                    Absent.TStack("Aimless"),
                ];
                card.greetMessages =
                [
                    "*wombat noises*"
                ];
            });
    }
}