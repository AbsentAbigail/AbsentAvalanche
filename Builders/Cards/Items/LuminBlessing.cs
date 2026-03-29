#region

using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Builders.StatusEffects;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Builders.Cards.Items;

[UsedImplicitly]
public class LuminBlessing : ICardBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateItem(Name, "Lumin Blessing")
            .SetDamage(null)
            .WithPlayType(Card.PlayType.None)
            .SetSprites(Absent.GetSprite("LuminBlessing"), Absent.GetSprite("LuminBlessingBG"))
            .WithPools(CardPools.GeneralItems)
            .WithValue(50)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Absent.SStack(OnCampaignWinLeaderStartsWithCharm.Name)
                ];
                card.needsTarget = false;
                card.canPlayOnBoard = false;
                card.canPlayOnHand = false;
                card.canPlayOnFriendly = false;
                card.canPlayOnEnemy = false;
            });
    }
}