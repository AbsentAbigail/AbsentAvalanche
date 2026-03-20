#region

using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Builders.StatusEffects;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Builders.Cards.Items;

[UsedImplicitly]
public class NebulaInstrumenta : ICardBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateItem(Name, "Nebula Instrumenta")
            .SetDamage(null)
            .NeedsTarget(false)
            .SetSprites(Absent.GetSprite("NebulaAuxilium"), Absent.GetSprite("NebulaAuxiliumBG"))
            .WithFlavour("Space donut")
            .WithValue(50)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.traits =
                [
                    Absent.TStack("Zoomlin"),
                    Absent.TStack("Consume")
                ];
                card.startWithEffects =
                [
                    Absent.SStack(OnCardPlayedTutorRandomTreasure.Name)
                ];
            });
    }
}