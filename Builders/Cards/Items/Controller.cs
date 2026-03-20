#region

using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Builders.Traits;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Builders.Cards.Items;

[UsedImplicitly]
public class Controller : ICardBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateItem(Name, "Controller")
            .SetDamage(2)
            .SetSprites(Absent.GetSprite("Controller"), Absent.GetSprite("ControllerBG"))
            .WithPools(CardPools.GeneralItems)
            .WithValue(50)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.traits =
                [
                    Absent.TStack(Combo.Name, 2),
                    Absent.TStack("Noomlin")
                ];
            });
    }
}