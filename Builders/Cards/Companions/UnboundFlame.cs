#region

using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Builders.StatusEffects;
using AbsentAvalanche.Helpers;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Builders.Cards.Companions;

[UsedImplicitly]
public class UnboundFlame : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateUnit(Name, "Unbound Flame",
                bloodProfile: "Blood Profile Black",
                idleAnim: "FloatAnimationProfile")
            .SetStats(5, 0, 3)
            .SetSprites(
                Absent.GetSprite("UnboundFlame"),
                Absent.GetSprite("UnboundFlameBG"))
            .WithFlavour(Flavour)
            .DropsBling(4)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.attackEffects = [Absent.SStack("Overload", 3)];
                card.startWithEffects =
                [
                    Absent.SStack(OnCardPlayedApplyOverloadToAlliesInRow.Name, 3)
                ];
                card.traits = [Absent.TStack("Barrage")];
                card.cardType = Absent.GetCardType("Summoned");
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public const string Flavour = "Ethereal flames fill the space around";
}