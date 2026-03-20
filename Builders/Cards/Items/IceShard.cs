#region

using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Builders.StatusEffects;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Builders.Cards.Items;

[UsedImplicitly]
public class IceShard : ICardBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateItem(Name, "Ice Shard", idleAnim: "Heartbeat2AnimationProfile")
            .SetDamage(1)
            .NeedsTarget(false)
            .SetSprites(Absent.GetSprite("IceShard"), Absent.GetSprite("IceShardBG"))
            .WithPools(CardPools.GeneralItems)
            .WithValue(50)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.attackEffects = [Absent.SStack("Frost")];
                card.startWithEffects = [Absent.SStack(HitsAllAlliesAndEnemies.Name)];
            });
    }
}