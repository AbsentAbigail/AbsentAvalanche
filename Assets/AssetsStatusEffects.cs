using System.Collections.Generic;
using AbsentAvalanche.StatusEffects;

namespace AbsentAvalanche.Assets;

public static class AssetsStatusEffects
{
    public static void AddToAssets(List<object> assets)
    {
        assets.AddRange([
            new Ethereal().Builder(),
            new Cat().Builder(),
            new Calm().Builder(),
            new FakeCalm().Builder(),
            new Abduct().Builder(),

            new OnCardPlayedGainOverload().Builder(),
            new WhenDestroyedSummonUnboundFlame().Builder(),
            new InstantSummonUnboundFlame().Builder(),
            new SummonUnboundFlame().Builder(),
            new OnCardPlayedApplyOverloadToAlliesInRow().Builder(),

            new TriggerAgainstTargetWhenMissileAttacks().Builder(),
            new WhileActiveMissilesHaveCat().Builder(),
            new WhileActiveItemsHaveCat().Builder(),
            new WhenEnemyIsHitByItemApplyWeaknessToThem().Builder(),

            new OnCardPlayedAddMissileToHand().Builder(),
            new InstantSummonMissileInHand().Builder(),
            new SummonMissile().Builder(),
            new GainCatWhenMissileIsPlayed().Builder(),
            new GainCatWhenItemIsPlayed().Builder(),
            new OnCardPlayedAddCatomicBombToHand().Builder(),
            new InstantSummonCatomicBombInHand().Builder(),
            new SummonCatomicBomb().Builder(),

            new WhenDeployedGainShellForEachEnemy().Builder(),
            new WhenDeployedGainSnowForEachEnemy().Builder(),

            new GoldRushEffect().Builder(),

            new HitsAllAlliesAndEnemies().Builder(),

            new OnHitGainEqualBling().Builder(),

            new WhenDestroyedDealDamageToRandomAlly().Builder(),

            new IncreaseEtherealToMatchRest().Builder(),

            new TriggerNoTrigger().Builder(),
            new OnKillTriggerNoTrigger().Builder(),

            new OnCardPlayedGainCat().Builder(),

            new WhileInHandApplyOverburnToRandomEnemy().Builder(),

            new WhenDeployedReduceCounterPerAlliedCompanion().Builder(),
            new WhenKilledInsteadGainScrap().Builder(),
            new WhenDeployedGainHealthPerAlliedCompanion().Builder(),
            new HitHighestAttack().Builder(),

            new TriggerWhenAllyBehindTriggers().Builder(),

            new SummonSarcophagus().Builder(),
            new InstantSummonSarcophagus().Builder(),
            new WhenDestroyedSummonSarcophagus().Builder(),

            new OnKillApplyCalmToSelf().Builder(),
            new OnTurnApplyCalmToAllyInFrontOf().Builder(),

            new InstantSummonUFOInHand().Builder(),
            new OnTurnSummonUFOInHand().Builder(),
            new SummonUFO().Builder(),

            new InstantIncreaseCurrentCounter().Builder(),

            new InstantEat().Builder(),
            new OnHitEat().Builder(),

            new Stress().Builder(),

            new OnCardPlayedDoubleAllCat().Builder(),
            new InstantDoubleCat().Builder(),

            new InstantAddCharmToInventory().Builder(),
            new InstantGainRandomCharm().Builder(),
            new OnBossKillGainRandomCharm().Builder(),

            new OnKillGainCat().Builder(),

            new WhenAnythingSnowedGainBlock().Builder(),
            new WhenEnemyIsKilledGainBlock().Builder(),
            new WhileActiveGainFrenzyEqualToBlock().Builder(),

            new SummonBlahaj().Builder(),
            new InstantSummonBlahaj().Builder(),
            new WhenHitSummonBlahaj().Builder(),

            new DoubleStatusEffectsAppliedToCatcus().Builder(),

            new WhenAllyHitGainFrenzy().Builder(),
            new InstantTransformIntoBamAndBoozle().Builder(),
            new WhenDeployedSplitIntoBamAndBoozle().Builder(),
            new InstantTransformIntoBamAndBoozleAscended().Builder(),
            new WhenDeployedSplitIntoBamAndBoozleAscended().Builder(),

            new InstantPermanentlyIncreaseHealth().Builder(),
            new OnKillIncreaseHealthPermanent().Builder(),

            new WhenAllyAheadGainsStatusApplyItToAllies().Builder(),

            new WhenAllyHitIncreaseEffects().Builder(),
            new InstantCountDownSnowFrostBlock().Builder(),
            new EveryTurnCountDownSnowFrostBlock().Builder(),

            new InstantTutorDeck().Builder(),
            new InstantTutorDeckCopyZoomlinConsume().Builder(),
            new InstantTutorDiscard().Builder(),
            new InstantTutorTenRandomCardsZoomlin().Builder(),
            new OnCardPlayedTutorDeckCopyConsumeZoomlin().Builder(),
            new OnCardPlayedTutorRandomCardZoomlin().Builder(),
            new InstantSummonDummyToHand().Builder(),
            new DummySummon().Builder(),
            new InstantTutorThreeRandomCompanions().Builder(),
            new OnCardPlayedTutorRandomCompanion().Builder(),
            new InstantTutorThreeRandomTreasures().Builder(),
            new OnCardPlayedTutorRandomTreasure().Builder(),

            new TemporarySafeConsume().Builder(),
            new TemporarySafeZoomlin().Builder(),

            new OnCardPlayedBoostSelf().Builder(),

            new SummonPillow().Builder(),
            new InstantSummonPillowInHand().Builder(),
            new WhenHitSummonPillow().Builder(),

            new InstantCleanseText().Builder(),
            new InstantHeadpat().Builder(),
            
            new InstantDrawAndApplyFrenzyAndAimless().Builder(),
            new OnCardPlayedDrawAndApplyFrenzyAndAimless().Builder(),
            
            new WhenAllyGainsNegativeStatusApplyToSelfInstead().Builder(),
            new OnCardPlayedCleanseSelf().Builder(),
            new WhenAnAllyGainsAPositiveStatusShareHalfToSelf().Builder(),
            
            new WhenDeployedSplitIntoCatcusAndCatcitten().Builder(),
            new InstantTransformIntoCatcusAndCatcitten().Builder(),
            new WhenDeployedSplitIntoCatcusAndCatcittenAscended().Builder(),
            new InstantTransformIntoCatcusAndCatcittenAscended().Builder(),
            new WhenEnemyIsKilledApplyTeethToAttacker().Builder(),
            new WhenEnemyIsKilledCountDownAttacker().Builder(),
            new CountDownASAP().Builder(),
            
            new OnCardPlayedGainSnow().Builder(),
            new OnCardPlayedTriggerAllyAhead().Builder(),
            new InstantTransformIntoBubblesAndCuddles().Builder(),
            new WhenDeployedSplitIntoBubblesAndCuddles().Builder(),
            new InstantTransformIntoBubblesAndCuddlesAscended().Builder(),
            new WhenDeployedSplitIntoBubblesAndCuddlesAscended().Builder(),
            
            new InstantTransformIntoSherbaAndCuddles().Builder(),
            new WhenDeployedSplitIntoSherbaAndCuddles().Builder(),
            new InstantTransformIntoSherbaAndCuddlesAscended().Builder(),
            new WhenDeployedSplitIntoSherbaAndCuddlesAscended().Builder(),
            new WhenDeployedApplySnowToEnemies().Builder(),
        ]);
    }
}