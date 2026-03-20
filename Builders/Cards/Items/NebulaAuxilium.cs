#region

using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Builders.StatusEffects;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Builders.Cards.Items;

[UsedImplicitly]
public class NebulaAuxilium : ICardBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public const string Flavour = "Space donut";

    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateItem(Name, "Nebula Auxilium")
            .SetDamage(null)
            .NeedsTarget(false)
            .SetSprites(Absent.GetSprite("NebulaAuxilium"), Absent.GetSprite("NebulaAuxiliumBG"))
            .WithFlavour(Flavour)
            .WithValue(50)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.AddToPets();
                card.traits =
                [
                    Absent.TStack("Zoomlin"),
                    Absent.TStack("Consume")
                ];
                card.startWithEffects =
                [
                    Absent.SStack(OnCardPlayedTutorRandomCompanion.Name)
                ];
                card.greetMessages =
                [
                    "As you stare into the void, the void stares back"
                ];
            });
    }
}