#region

using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Builders.StatusEffects;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Builders.Cards.Items;

[UsedImplicitly]
public class LuminBooties : ICardBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateItem(Name, "Lumin Booties")
            .SetDamage(null)
            .SetSprites(Absent.GetSprite("LuminBooties"), Absent.GetSprite("LuminBootiesBG"))
            .WithValue(50)
            .CanPlayOnHand(false)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Absent.SStack(Equip.Name),
                    Absent.SStack("ImmuneToSnow"),
                    Absent.SStack(WhenAllySnowedCountDownSelf.Name),
                    Absent.SStack(WhenEquippedIncreaseEffects.Name),
                ];
            });
    }
}