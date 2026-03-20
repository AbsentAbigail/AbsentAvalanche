#region

using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Builders.StatusEffects;
using AbsentAvalanche.Builders.Traits;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Builders.Cards.Items;

[UsedImplicitly]
public class FireSpell : ICardBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateItem(Name, "Fire Spell")
            .SetDamage(0)
            .SetSprites(Absent.GetSprite("FireSpell"), Absent.GetSprite("FireSpellBG"))
            .WithPools(CardPools.ShademancerItems)
            .WithValue(50)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Absent.SStack(OnCardPlayedApplyOverloadToTarget.Name),
                    Absent.SStack(OnCardPlayedGainApplyOverburn.Name)
                ];
                card.traits =
                [
                    Absent.TStack(Combo.Name, 2),
                    Absent.TStack("Noomlin"),
                    Absent.TStack("Consume")
                ];
            });
    }
}