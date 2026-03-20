#region

using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Builders.Keywords;
using AbsentAvalanche.Builders.StatusEffects;
using AbsentAvalanche.Helpers;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using Cat = AbsentAvalanche.Builders.StatusEffects.Cat;

#endregion

namespace AbsentAvalanche.Builders.Cards.Companions;

[UsedImplicitly]
public class Catcus : ICardBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public const string Flavour = "Sun loving, spiky feline with an adventurous Spirit! :3";

    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateUnit(Name, "Catcus",
                "TargetModeBasic",
                "Blood Profile Pink Wisp")
            .SetStats(6, 0, 4)
            .SetSprites(
                Absent.GetSprite("Catcus"),
                Absent.GetSprite("CatcusBG"))
            .WithFlavour(Flavour)
            .DropsBling(4)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.AddToPets();
                card.startWithEffects =
                [
                    Absent.SStack("Teeth"),
                    Absent.SStack(Cat.Name),
                    Absent.SStack("MultiHit"),
                    Absent.SStack(OnKillGainCat.Name)
                ];
                card.createScripts =
                [
                    LeaderHelper.GiveUpgrade()
                ];
            })
            .WithText(Absent.KeywordTag(Royal.Name))
            .WithCardType("Leader");
    }
}