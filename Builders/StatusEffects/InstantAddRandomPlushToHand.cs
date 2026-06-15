using System.Linq;
using AbsentAvalanche.Builders.Cards.Clunkers;
using AbsentAvalanche.Builders.Cards.Companions;
using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.StatusEffectImplementations;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using Patch = AbsentAvalanche.Builders.Cards.Companions.Patch;

namespace AbsentAvalanche.Builders.StatusEffects;

[UsedImplicitly]
public class InstantAddRandomPlushToHand : IStatusBuilder
{
    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Absent.Instance)
            .Create<StatusEffectInstantSummonRandomFromPool>(Name)
            .WithStackable(true)
            .WithCanBeBoosted(false)
            .SubscribeToAfterAllBuildEvent<StatusEffectInstantSummonRandomFromPool>(status =>
            {
                status.canSummonMultiple = true;
                status.targetSummon = Absent.GetStatusOf<StatusEffectSummonWithCrown>(SummonDummyWithCrown.Name);
                status.summonPosition = StatusEffectInstantSummon.Position.Hand;
                status.pool = GetCards(
                    Alice.Name,
                    Amber.Name,
                    April.Name,
                    Bam.Name,
                    Boozle.Name,
                    Bubbles.Name,
                    Catcitten.Name,
                    CatcittenFrenzy.Name,
                    CatcittenShell.Name,
                    CatcittenSnow.Name,
                    CatcittenSpice.Name,
                    CatcusZoom.Name,
                    Chirp.Name,
                    Coral.Name,
                    Cuddles.Name,
                    Emerald.Name,
                    Hearth.Name,
                    Jerry.Name,
                    Kiki.Name,
                    LilGuy.Name,
                    LongCat.Name,
                    LongKitty.Name,
                    May.Name,
                    Nami.Name,
                    Nina.Name,
                    Nova.Name,
                    OctaviaBlue.Name,
                    Patch.Name,
                    Pengu.Name,
                    Puppo.Name,
                    Sally.Name,
                    Sam.Name,
                    Seal.Name,
                    Sherba.Name,
                    Snowflake.Name,
                    Spot.Name,
                    Tiny.Name,
                    Tiramisu.Name
                );
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    
    
    private static CardData[] GetCards(params string[] cards)
    {
        return cards.Select(Absent.GetCard).ToArray();
    }
}