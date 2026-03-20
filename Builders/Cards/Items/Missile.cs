#region

using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Builders.StatusEffects;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Builders.Cards.Items;

[UsedImplicitly]
public class Missile : ICardBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateItem(Name, "Missile")
            .SetDamage(1)
            .SetSprites(Absent.GetSprite("Missile"), Absent.GetSprite("MissileBG"))
            .WithValue(50)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.traits = [Absent.TStack("Consume")];
                card.startWithEffects =
                [
                    Absent.SStack(TriggerAgainstTargetWhenMissileAttacks.Name)
                ];
            });
    }
}