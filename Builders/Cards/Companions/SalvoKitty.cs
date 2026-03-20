#region

using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Builders.StatusEffects;
using AbsentAvalanche.Helpers;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using Cat = AbsentAvalanche.Builders.StatusEffects.Cat;

#endregion

namespace AbsentAvalanche.Builders.Cards.Companions;

[UsedImplicitly]
public class SalvoKitty : ICardBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public const string Flavour = "\"Who gave this cat this button?\"";

    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateUnit(Name, "Salvo Kitty")
            .SetStats(4, 0, 3)
            .SetSprites(
                Absent.GetSprite("SalvoKitty"),
                Absent.GetSprite("SalvoKittyBG"))
            .WithFlavour(Flavour)
            .DropsBling(4)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.AddToPets();
                card.startWithEffects =
                [
                    Absent.SStack(Cat.Name),
                    Absent.SStack(GainCatWhenMissileIsPlayed.Name),
                    Absent.SStack(OnCardPlayedAddMissileToHand.Name, 2),
                ];
            });
    }
}