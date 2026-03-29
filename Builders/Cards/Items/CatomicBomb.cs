#region

using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Builders.StatusEffects;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Builders.Cards.Items;

[UsedImplicitly]
public class CatomicBomb : ICardBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateItem(Name, "Catomic Bomb")
            .SetDamage(0)
            .SetSprites(Absent.GetSprite("CatomicBomb"), Absent.GetSprite("CatomicBombBG"))
            .WithPools(CardPools.GeneralItems)
            .WithValue(50)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.attackEffects =
                [
                    Absent.SStack(Cat.Name, 2)
                ];
                card.startWithEffects =
                [
                    Absent.SStack(Cat.Name),
                    Absent.SStack(OnCardPlayedDoubleAllCat.Name),
                    Absent.SStack(HitsAllAlliesAndEnemies.Name)
                ];
            });
    }
}