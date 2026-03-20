#region

using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Builders.StatusEffects;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Builders.Cards.Items;

[UsedImplicitly]
public class ForgottenBox : ICardBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public const string Flavour = "A cardboard box under the bed... What could be inside?";

    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateItem(Name, "Forgotten Box")
            .SetDamage(null)
            .NeedsTarget(false)
            .SetSprites(Absent.GetSprite("ShadyBox"), Absent.GetSprite("ShadyBoxBG"))
            .WithFlavour(Flavour)
            .WithValue(50)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Absent.SStack(Ethereal.Name, 4),
                    Absent.SStack("When Destroyed Apply Frenzy To RandomAlly")
                ];
            });
    }
}