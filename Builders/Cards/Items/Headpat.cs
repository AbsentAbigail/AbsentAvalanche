#region

using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Builders.StatusEffects;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Builders.Cards.Items;

[UsedImplicitly]
public class Headpat : ICardBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public const string Flavour = "There there, you did well!";

    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateItem(Name, "Headpat")
            .SetDamage(null)
            .SetSprites(Absent.GetSprite("Headpat"), Absent.GetSprite("HeadpatBG"))
            .WithFlavour(Flavour)
            .WithValue(50)
            .CanPlayOnHand(false)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.attackEffects =
                [
                    Absent.SStack("Heal", 2),
                    Absent.SStack(InstantCleanseText.Name),
                    Absent.SStack(InstantHeadpat.Name)
                ];
                card.traits = [Absent.TStack("Draw")];
            });
    }
}