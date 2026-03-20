#region

using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Builders.Keywords;
using AbsentAvalanche.Helpers;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using Cat = AbsentAvalanche.Builders.StatusEffects.Cat;
using DreamTeam = AbsentAvalanche.Builders.StatusEffects.DreamTeam;

#endregion

namespace AbsentAvalanche.Builders.Cards.Companions;

[UsedImplicitly]
public class Catci : ICardBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public const string Flavour = "Prepare for trouble, and make it double";

    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateUnit(Name, "Catci", bloodProfile: "Blood Profile Pink Wisp")
            .SetStats(10, 0, 4)
            .SetSprites(
                Absent.GetSprite("Catci"),
                Absent.GetSprite("CatciBG"))
            .WithFlavour(Flavour)
            .DropsBling(4)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Absent.SStack(Cat.Name, 2),
                    Absent.SStack("MultiHit"),
                    Absent.SStack(DreamTeam.NameWhenDeployed(Catcus.Name, Catcitten.Name))
                ];
                card.createScripts =
                [
                    LeaderHelper.GiveUpgrade()
                ];
                card.charmSlots *= 2;
            })
            .WithText(Absent.KeywordTag(Royal.Name))
            .WithCardType("Leader");
    }
}