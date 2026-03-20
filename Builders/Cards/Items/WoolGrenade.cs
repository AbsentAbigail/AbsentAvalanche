#region

using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Builders.StatusEffects;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Builders.Cards.Items;

[UsedImplicitly]
public class WoolGrenade : ICardBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateItem(Name, "Wool Grenade")
            .SetDamage(0)
            .SetSprites(Absent.GetSprite("WoolGrenade"), Absent.GetSprite("WoolGrenadeBG"))
            .WithValue(50)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.attackEffects = [Absent.SStack("Weakness")];
                card.startWithEffects =
                [
                    Absent.SStack(Ethereal.Name, 4),
                    Absent.SStack(WhenDestroyedApplyWeaknessToEnemies.Name, 3)
                ];
            });
    }
}