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
public class GhostlyPresence : ICardBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateItem(Name, "Ghostly Presence")
            .SetDamage(null)
            .NeedsTarget(false)
            .SetSprites(Absent.GetSprite("GhostlyPresence"), Absent.GetSprite("GhostlyPresenceBG"))
            .WithPools(CardPools.ShademancerItems)
            .WithValue(50)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.traits = [Absent.TStack(Rest.Name, 3)];
                card.startWithEffects =
                [
                    Absent.SStack(Ethereal.Name, 3),
                    Absent.SStack(WhileInHandApplyOverburnToRandomEnemy.Name, 3)
                ];
            });
    }
}