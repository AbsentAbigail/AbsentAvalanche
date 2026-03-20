#region

using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Builders.StatusEffects;
using AbsentAvalanche.Helpers;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Builders.Cards.Companions;

[UsedImplicitly]
public class LongKitty : ILeaderBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateUnit(Name, "Long Kitty",
                "TargetModeBasic",
                "Blood Profile Snow")
            .SetStats(4, 1, 3)
            .SetSprites(
                Absent.GetSprite("LongKitty"),
                Absent.GetSprite("LongKittyBG"))
            .WithFlavour(Flavour)
            .DropsBling(4)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Absent.SStack(OnCardPlayedSplit.Name)
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public const string Flavour = "A bit shy but very friendly";

    public bool LeaderExclusive => false;

    public bool InPool => false;
    
    public ILeaderBuilder.LeaderModifier LeaderModifiers => new();
}