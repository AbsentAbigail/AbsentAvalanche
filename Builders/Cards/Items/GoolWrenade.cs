#region

using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Builders.StatusEffects;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Builders.Cards.Items;

[UsedImplicitly]
public class GoolWrenade : ICardBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public const string Flavour = "Wait, this isn't right...";

    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateItem(Name, "Ghostly Presence")
            .SetDamage(null)
            .SetSprites(Absent.GetSprite("GoolWrenade"), Absent.GetSprite("GoolWrenadeBG"))
            .WithFlavour(Flavour)
            .WithValue(50)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Absent.SStack(Ethereal.Name, 4),
                    Absent.SStack(WhenDestroyedApplyWeaknessToAllies.Name)
                ];
            });
    }
}