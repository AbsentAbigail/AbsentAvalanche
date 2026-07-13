using System.Linq;
using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.StatusEffectImplementations;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

namespace AbsentAvalanche.Builders.StatusEffects;

[UsedImplicitly]
public class InstantMetronome : IStatusBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Absent.Instance)
            .Create<StatusEffectMetronome>(Name)
            .WithStackable(false)
            .WithCanBeBoosted(false)
            .SubscribeToAfterAllBuildEvent<StatusEffectMetronome>(status =>
            {
                status.movePool =
                [
                    Move("Pay Day", "Gain [{0}]<sprite name=bling>", OnCardPlayedGainBling.Name, 4),
                    Move("Helping Hand", "Apply [{0}]<sprite name=spice> to all allies", "On Card Played Apply Spice To Allies", 2),
                    Move("Flame Wheel", "[{0}]<sprite name=spice>", "Spice", 4),
                    Move("Ice Beam", "Apply [{0}]<sprite name=snow>", InstantGainApplySnow.Name, 1, 1),
                    Move("Wide Guard", "Apply [{0}]<sprite name=shell> to all allies", "On Card Played Apply Shell To Allies", 2),
                    Move("Ember", "Apply [{0}]<sprite name=overload>", InstantGainApplyOverburn.Name, 1),
                    Move("Flamethrower", "Apply [{0}]<sprite name=overload>", InstantGainApplyOverburn.Name, 2),
                    Move("Poison Powder", "Apply [{0}]<sprite name=shroom>", InstantGainApplyShroom.Name, 2),
                    Move("Toxic", "Apply [{0}]<sprite name=shroom>", InstantGainApplyShroom.Name, 3),
                    Move("Confusion", "Apply [{0}]<sprite name=haze>", InstantGainApplyHaze.Name, 1, 1),
                    Move("Shadow Ball", "Apply [{0}]<sprite name=demonize>", InstantGainApplyDemonize.Name, 1, 1),
                    Move("Metal Sound", "Apply [{0}]<sprite name=demonize>", InstantGainApplyDemonize.Name, 2),
                    Move("Tail Whip", "Apply [{0}]<sprite name=vim>", InstantGainApplyBom.Name, 1),
                    Move("Screech", "Apply [{0}]<sprite name=vim>", InstantGainApplyBom.Name, 2),
                    Move("Powdered Snow", "Apply [{0}]<sprite name=snow>", InstantGainApplyFrost.Name, 2),
                    Move("Tri Attack", "Apply [{0}]<sprite name=snow>, [{0}]<sprite name=overload>, [{0}]<sprite name=ink>", [InstantGainApplySnow.Name, InstantGainApplyOverburn.Name, InstantGainApplyInk.Name], 1),
                    Move("Dire Claw", "Apply [{0}]<sprite name=frost>, [{0}]<sprite name=ink>, [{0}]<sprite name=shroom>", [InstantGainApplyFrost.Name, InstantGainApplyInk.Name, InstantGainApplyShroom.Name], 1),
                    Move("Self Destruct", "Consume, deal [{0}] damage to all enemies", [OnCardPlayedDealDamageToAllEnemies.Name, TemporarySafeConsume.Name], 4, 2),
                    Move("Dragon Dance",null, [], 0, 1),
                    Move("Swords Dance",null, [], 0, 2),
                    Move("Fury Cutter", "Gain [+{0}]<sprite name=attack>", "On Card Played Apply Attack To Self", 1),
                    Move("Icy Wind", "Apply [{0}]<sprite name=snow> to enemies in row", "On Card Played Apply Snow To EnemiesInRow", 1),
                    Move("Assist", "[{0}]<sprite name=cat>", Cat.Name, 3),
                    Move("Soft Boiled", "[+{0}]<sprite name=health>", "Increase Max Health", 3),
                    Move("Harden", "[{0}]<sprite name=shell>", "Shell", 3),
                    Move("Iron Defense", "[+{0}]<sprite name=shell>", "Shell", 6),
                    Move("Protect", "[{0}]<sprite name=block>", "Block", 1),
                    Move("Spiky Shield", "[{0}]<sprite name=block> and [{0}]<sprite name=teeth>", ["Block", "Teeth"], 1),
                    Move("Rest", "[{0}]<sprite name=snow>, after being cleansed and healed to full", ["Cleanse", "Snow", InstantHealFull.Name], 2),
                    Move("Bulk Up", "[{0}]<sprite name=shell>", "Shell", 1, 1),
                    Move("Ingrain", "Hogheaded, restore [{0}]<sprite name=health> every turn", [TemporarySafeHogheaded.Name, EveryTurnRestoreHealth.Name], 1),
                    Move("Aqua Ring", "restore [{0}]<sprite name=health> every turn", EveryTurnRestoreHealth.Name, 1),
                    Move("Fly", "[{0}]<sprite name=abduct>", Abduct.Name, 1, 2),
                    Move("Rock Polish", "[{0}]<sprite name=calm>", Calm.Name, 3),
                    Move("Agility", "Noomlin", TemporarySafeNoomlin.Name, 1),
                    Move("Recycle", "Combo [{0}]", TemporarySafeCombo.Name, 1),
                    Move("Will-O-Wisp", "Apply [{0}]<sprite name=overload> to all enemies", OnCardPlayedApplyOverburnToAllEnemies.Name, 1),
                    Move("Calm Mind", "[+{0}] to its effects", OngoingIncreaseEffectsWithTargetConstraints.Name, 1),
                    Move("Nasty Plot", "[+{0}] to its effects", OngoingIncreaseEffectsWithTargetConstraints.Name, 2),
                    Move("Splash", "nothing", [], 0),
                    Move("Stealth Rock", "when an enemy enters battle, deal [{0}] damage to it", WhenEnemyEntersBattleDealDamage.Name, 1),
                    Move("Sticky Web", "when an enemy enters battle, increase its <sprite name=counter> by [{0}]", WhenEnemyEntersBattleIncreaseItsCounter.Name, 1),
                    Move("Toxic Spikes", "when an enemy enters battle, apply [{0}]<sprite name=shroom> to it", WhenEnemyEntersBattleApplyShroom.Name, 1),
                    Move("Moongeist Beam", "[{0}]<sprite name=ink>", InstantGainApplyInk.Name, 1, 1),
                    Move("Moonblast", "reduce the target's effects by [{0}]", InstantGainFrostbiteShard.Name, 1, 1),
                    Move("Thunderbolt", "deal [{0}] additional damage", InstantGainDealAdditionalDamage.Name, 1),
                    Move("Surf", "deal [{0}] damage to all enemies", OnCardPlayedDealDamageToAllEnemies.Name, 1),
                    Move("Dream Eater", "deal [{0}] additional damage to <sprite name=snow>'d targets", "On Hit Damage Snowed Target", 8),
                    Move("Scald", "Apply [{0}]<sprite name=frost>, [{0}]<sprite name=overload>", [InstantGainApplyFrost.Name, InstantGainApplyOverburn.Name], 1),
                    Move("Magnetise", "Gain [{0}]<sprite name=flight>", OnCardPlayedGainFlight.Name, 1),
                    Move("Hyper Beam", "Gain [{0}]<sprite name=snow>", OnCardPlayedGainSnow.Name, 1, 4),
                    Move("Giga Impact", "Gain [{0}]<sprite name=snow>", OnCardPlayedGainSnow.Name, 1, 4),
                    Move("Heal Bell", "cleanse all allies", OnCardPlayedCleanseAllies.Name, 1),
                    Move("Heal Pulse", "restore [{0}]<sprite name=health> to a random ally", OnCardPlayedHealRandomAlly.Name, 1),
                    Move("Absorb", "restore <sprite name=health> to front ally equal to damage dealt", "On Hit Equal Heal To FrontAlly", 1, 1),
                    Move("Mega Drain", "restore <sprite name=health> to front ally equal to damage dealt", "On Hit Equal Heal To FrontAlly", 1, 2),
                    Move("Brick Break", "remove <sprite name=block> and <sprite name=shell> from the target", InstantGainRemoveShellAndBlock.Name, 1, 1),
                    Move("Trick-or-Treat", "apply Summoned", InstantGainApplySummoned.Name, 1),
                    Move("Metronome", "another metronome roll", Name, 1),
                    Move("Strength Sap", "Apply [{0}]<sprite name=frost>, restore [{0}]<sprite name=health> to self", [InstantGainApplyFrost.Name, OnCardPlayedHealSelf.Name], 1),
                    Move("Refresh", "Cleanse", OnCardPlayedCleanseSelf.Name, 1),
                    Move("Close Combat", "[{0}]<sprite name=demonize>, [{0}]<sprite name=vim>", ["Weakness", "Demonize"], 1, 4),
                ];
                status.criticalMovePool =
                [
                    Move("Flare Blitz", "[{0}]<sprite name=spice>", "Spice", 10),
                    Move("Blizzard", "Apply [{0}]<sprite name=snow>", InstantGainApplySnow.Name, 4, 2),
                    Move("Instruct", "Add [x{0}]<sprite name=frenzy> to a random ally", OnCardPlayedAddFrenzyToRandomAlly.Name, 1),
                    Move("Fire Blast", "Apply [{0}]<sprite name=overload>", InstantGainApplyOverburn.Name, 4),
                    Move("Dual Chop", "[+x{0}]<sprite name=frenzy>", "MultiHit", 1, 1),
                    Move("Overheat", "Replace <sprite name=attack> with <sprite name=overload>", InstantReplaceAttackWithOverburn.Name, 1, 1),
                    Move("Explode", "Consume, deal [{0}] damage to all enemies", [OnCardPlayedDealDamageToAllEnemies.Name, TemporarySafeConsume.Name], 12, 4),
                    Move("Belly Drum", "[x2]<sprite name=attack>, but halved its <sprite name=health>", ["Lose Half Health", "Double Attack"], 1),
                    Move("Transform", "Summon a copy of an enemy on your side with [{0}]<sprite name=health>", InstantTransform.Name, 1),
                    Move("Fusion Bolt", "Deal [{0}] additional damage to targets on 1<keyword=counter>", OnHitDamageTargetOn1Counter.Name, 6),
                    Move("Fusion Flare", "Apply [{0}]<sprite name=overload>, double <sprite name=overload>", [InstantGainApplyOverburn.Name, InstantGainDoubleOverload.Name], 1),
                    Move("Judgement", "Hits all enemies", "Hit All Enemies", 1, 1),
                    Move("Dark Void", "Apply [{0}]<sprite name=ink> to all enemies", OnCardPlayedApplyInkToEnemies.Name, 3),
                    Move("Tail Glow", "[+{0}] to its effects", OngoingIncreaseEffectsWithTargetConstraints.Name, 3),
                    Move("Wave Crash",null, [], 0, 6),
                    Move("Thunder", "deal [{0}] additional damage", InstantGainDealAdditionalDamage.Name, 5),
                    Move("Giga Drain", "Restore <sprite name=health> to front ally equal to damage dealt", "On Hit Equal Heal To FrontAlly", 1, 3),
                    Move("Bitter Blade", "Restore <sprite name=health> to front ally equal to damage dealt", "On Hit Equal Heal To FrontAlly", 1, 3),
                    Move("Swagger", "Apply [{0}]<sprite name=haze>, increase targets <sprite name=attack> by [{0}]", [InstantGainApplyHaze.Name, InstantGainIncreaseAttack.Name], 2),
                    Move("Leech Seed", "Apply Leech Seed [{0}]", InstantGainApplyLeechSeed.Name, 1),
                    Move("Me first", "a random ally triggers against the target", InstantGainRandomAllyTriggersAgainstTarget.Name, 1),
                    Move("Sheer Cold", "1/3 chance to kill", InstantGainSheerCold.Name, 1),
                ];
            });
    }

    private static StatusEffectMetronome.Move Move(string name, string description, string effect, int count, int increaseAttack = 0)
    {
        return new StatusEffectMetronome.Move { name = name, description = description, effects = [Absent.GetStatus(effect)], count = count, increaseAttack = increaseAttack };
    }
    
    private static StatusEffectMetronome.Move Move(string name, string description, string[] effects, int count, int increaseAttack = 0)
    {
        return new StatusEffectMetronome.Move { name = name, description = description, effects = effects.Select(Absent.GetStatus).ToArray(), count = count, increaseAttack = increaseAttack };
    }
}