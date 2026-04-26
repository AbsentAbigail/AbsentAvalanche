#region

using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Builders.StatusEffects;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Builders.Cards.Items;

[UsedImplicitly]
public class VengefulAxe : ICardBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateItem(Name, "Vengeful Axe")
            .SetDamage(1)
            .SetSprites(Absent.GetSprite("VengefulAxe"), Absent.GetSprite("VengefulAxeBG"))
            .WithValue(50)
            .CanPlayOnHand(false)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Absent.SStack(Equip.Name),
                    Absent.SStack("Demonize", 3),
                    Absent.SStack(WhenEquipeeDiesGainHalfAttack.Name)
                ];
            });
    }
}