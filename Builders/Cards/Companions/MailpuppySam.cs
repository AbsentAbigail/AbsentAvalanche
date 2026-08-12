using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Helpers;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using UnityEngine;
using Mailpuppy = AbsentAvalanche.Builders.Traits.Mailpuppy;

namespace AbsentAvalanche.Builders.Cards.Companions;

[UsedImplicitly]
public class MailpuppySam : ILeaderBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public const string Flavour = "He's got a letter with your name on it";

    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateUnit(Name, "Mailpuppy Sam")
            .SetStats(8, 2, 6)
            .SetSprites(
                Absent.GetSprite("MailpuppySam"),
                Absent.GetSprite("MailpuppySamBG"))
            .WithFlavour(Flavour)
            .WithPools(CardPools.GeneralUnits)
            .DropsBling(4)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.traits =
                [
                    Absent.TStack(Mailpuppy.Name),
                ];
                card.greetMessages =
                [
                    "Will you help me deliver these letters?",
                ];
            });
    }
    
    public bool LeaderExclusive => false;

    public bool InPool => true;

    public ILeaderBuilder.LeaderModifier LeaderModifiers => new()
    {
        healthRange = new Vector2Int(-1, 1),
        damageRange = new Vector2Int(-1, 1),
        counterRange =  new Vector2Int(0, 1),
    };
}