using System.Collections.Generic;
using AbsentAvalanche.Cards.Clunkers;
using AbsentAvalanche.Cards.Companion;
using AbsentAvalanche.StatusEffects;
using AbsentUtilities;

namespace AbsentAvalanche.Assets;

public static class AssetsStatusEffects
{
    public static void AddToAssets(List<object> assets)
    {
        assets.AddRange([
            # region Statuses

            new Ethereal().Builder(),
            new Cat().Builder(),
            new Calm().Builder(),
            new FakeCalm().Builder(),
            new Abduct().Builder(),

            #endregion Statuses

            # region Dream Team
            
            .. DreamTeam.EffectBuilders(Bubbles.Name, Cuddles.Name), // Bubbles and Cuddles
            .. DreamTeam.EffectBuilders(Bam.Name, Boozle.Name), // Bam and Boozle
            .. DreamTeam.EffectBuilders(Catcus.Name, Catcitten.Name), // Catci
            .. DreamTeam.EffectBuilders(Sherba.Name, Cuddles.Name), // Snuggle Buddies
            .. DreamTeam.EffectBuilders(Alice.Name, Nami.Name), // The coziest pacas
            .. DreamTeam.EffectBuilders(April.Name, May.Name, // Sheep and Dino
                instant => {
                instant.ReplaceEffects =
                [
                    [AbsentUtils.SStack(OnCardPlayedAddWoolGrenadeToHand.Name),
                    AbsentUtils.SStack(OnCardPlayedAddGoolWrenadeToHand.Name)]
                ];
            }),
            
            #endregion Dream Team
            
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

            new InstantSummonUfoInHand().Builder(),
            new OnTurnSummonUfoInHand().Builder(),
            new SummonUfo().Builder(),

            new InstantIncreaseCurrentCounter().Builder(),

            new InstantEat().Builder(),
            new OnHitEat().Builder(),

            new DealAdditionalDamageForEachDamagedAlly().Builder(),

            new OnCardPlayedDoubleAllCat().Builder(),
            new InstantDoubleCat().Builder(),

            new InstantAddCharmToInventory().Builder(),
            new InstantGainRandomCharm().Builder(),
            new OnBossKillGainRandomCharm().Builder(),

            new OnKillGainCat().Builder(),

            new WhenEnemyIsKilledGainBlock().Builder(),
            new WhileActiveGainFrenzyEqualToBlock().Builder(),

            new SummonBlahaj().Builder(),
            new InstantSummonBlahaj().Builder(),
            new WhenHitSummonBlahaj().Builder(),

            new DoubleStatusEffectsAppliedToCatcus().Builder(),

            new WhenAllyHitGainFrenzy().Builder(),

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

            new WhenEnemyIsKilledApplyTeethToAttacker().Builder(),
            new WhenEnemyIsKilledCountDownAttacker().Builder(),
            new CountDownASAP().Builder(),

            new OnCardPlayedGainSnow().Builder(),
            new OnCardPlayedTriggerAllyAhead().Builder(),

            new WhenDeployedApplySnowToEnemies().Builder(),

            new WhenDestroyedApplyWeaknessToEnemies().Builder(),
            new SummonWoolGrenade().Builder(),
            new InstantSummonWoolGrenadeInHand().Builder(),
            new OnCardPlayedAddWoolGrenadeToHand().Builder(),
            new WhenDestroyedApplyWeaknessToAllies().Builder(),
            new SummonGoolWrenade().Builder(),
            new InstantSummonGoolWrenadeInHand().Builder(),
            new OnCardPlayedAddGoolWrenadeToHand().Builder(),
            new InstantCountDownEthereal().Builder(),
            new CountDownEtherealWhenDrawn().Builder(),
            new WhileActiveCountDownEtherealWhenDrawn().Builder(),

            new InstantRandomBuff().Builder(),
            new OnCardPlayedApplyRandomBuffToRandomAlly().Builder(),
            new WhenDeployedApplyRandomBuffToAllAllies().Builder(),
            
        ]);
    }
}