#region

using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Builders.StatusEffects;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Builders.Cards.Items;

[UsedImplicitly]
public class HeavySmash : ICardBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateItem(Name, "Squeaky Hammer")
            .SetDamage(4)
            .SetSprites(Absent.GetSprite("HeavySmash"), Absent.GetSprite("HeavySmashBG"))
            .WithPools(CardPools.GeneralItems)
            .WithValue(50)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.attackEffects =
                [
                    Absent.SStack(InstantPlaySqueakSound.Name)
                ];
                card.startWithEffects =
                [
                    Absent.SStack(OnKillGainCombo.Name)
                ];
                card.traits =
                [
                    Absent.TStack("Consume")
                ];
            });
    }
}