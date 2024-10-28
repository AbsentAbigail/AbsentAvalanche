using System.Collections.Generic;
using System.Linq;
using AbsentAvalanche.Cards.Companion;
using AbsentAvalanche.Cards.Items;
using AbsentAvalanche.CardUpgrades;
using AbsentAvalanche.Keywords;
using AbsentAvalanche.StatusEffects;
using AbsentUtilities;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using WildfrostHopeMod;
using WildfrostHopeMod.SFX;
using WildfrostHopeMod.Utils;
using WildfrostHopeMod.VFX;
using Abduct = AbsentAvalanche.Keywords.Abduct;
using Calm = AbsentAvalanche.Keywords.Calm;
using Cat = AbsentAvalanche.Keywords.Cat;
using Ethereal = AbsentAvalanche.Keywords.Ethereal;
using Valor = AbsentAvalanche.Traits.Valor;

namespace AbsentAvalanche;

public class Absent(string directory) : WildfrostMod(directory)
{
    private List<object> _assets;

    //this is here to allow our icon to appear in the text box of cards
    private TMP_SpriteAsset _assetSprites;
    private bool _loaded;

    [ConfigInput] [ConfigSlider(0f, 10f)] [ConfigItem(4f, null, "Val max size")] [UsedImplicitly]
    public float ValMaxSize;

    [ConfigInput] [ConfigSlider(0, 100)] [ConfigItem(40, null, "Attack needed for max Val size")] [UsedImplicitly]
    public int ValMaxSizeAt;

    [ConfigInput] [ConfigSlider(0f, 10f)] [ConfigItem(0.25f, null, "Val min size")] [UsedImplicitly]
    public float ValMinSize;

    public override string GUID => "absentabigail.wildfrost.absentavalanche";

    public override string[] Depends =>
        ["hope.wildfrost.vfx", "hope.wildfrost.configs", "absentabigail.wildfrost.absentutils"];

    public override string Title => "Absent Avalanche";

    public override string Description => GetDescription();
    public override TMP_SpriteAsset SpriteAsset => _assetSprites;

    public override void Load()
    {
        AbsentUtils.AddModInfo(new AbsentUtils.ModInfo
        {
            Mod = this,
            Prefix = "AbsentAvalanche"
        });

        if (!_loaded) CreateModAssets();
        base.Load();

        AbsentUtils.GetKeyword("immunetosnow").show = true;
        AbsentUtils.GetKeyword("immunetosnow").showName = true;

        var aimless = AbsentUtils.GetTrait("Aimless");
        var barrage = AbsentUtils.GetTrait("Barrage");
        var longshot = AbsentUtils.GetTrait("Longshot");
        var valor = AbsentUtils.GetTrait(Valor.Name);
        aimless.overrides = aimless.overrides.AddToArray(valor);
        barrage.overrides = aimless.overrides.AddToArray(valor);
        longshot.overrides = aimless.overrides.AddToArray(valor);

        //needed for custom icons
        var floatingText = Object.FindObjectOfType<FloatingText>(true);
        floatingText.textAsset.spriteAsset.fallbackSpriteAssets.Add(_assetSprites);
    }

    public override void Unload()
    {
        var aimless = AbsentUtils.GetTrait("Aimless");
        var barrage = AbsentUtils.GetTrait("Barrage");
        var longshot = AbsentUtils.GetTrait("Longshot");
        var valor = AbsentUtils.GetTrait(Valor.Name);
        aimless.overrides = aimless.overrides.RemoveFromArray(valor);
        barrage.overrides = aimless.overrides.RemoveFromArray(valor);
        longshot.overrides = aimless.overrides.RemoveFromArray(valor);

        UnloadFromClasses();
        base.Unload();

        AbsentUtils.GetKeyword("immunetosnow").show = false;
        AbsentUtils.GetKeyword("immunetosnow").showName = false;
    }

    public void CreateModAssets()
    {
        //Needed to get sprites in text boxes
        _assetSprites = HopeUtils.CreateSpriteAsset("AbsentAvalancheAssets", ImagePath(""));

        VFXHelper.VFX = new GIFLoader(this, ImagePath("Anim"));
        VFXHelper.VFX.RegisterAllAsApplyEffect();

        VFXHelper.SFX = new SFXLoader(ImagePath("Sounds"));
        VFXHelper.SFX.RegisterAllSoundsToGlobal();

        Ethereal.Data();

        StatusIconHelper.CreateIcon(
            "ethereal",
            ImagePath("ethereal.png").ToSprite(),
            "ethereal",
            1,
            "vim",
            new Color(0.2f, 0.2f, 0.3f),
            [AbsentUtils.GetKeyword(Ethereal.Name)]
        ).GetComponentInChildren<TextMeshProUGUI>(true).enabled = true;

        Cat.Data();

        StatusIconHelper.CreateIcon(
            "cat",
            ImagePath("cat.png").ToSprite(),
            "cat",
            1,
            "vim",
            new Color(0.2f, 0.2f, 0.3f),
            [AbsentUtils.GetKeyword(Cat.Name)]
        ).GetComponentInChildren<TextMeshProUGUI>(true).enabled = true;

        Calm.Data();

        StatusIconHelper.CreateIcon(
            "calm",
            ImagePath("calm.png").ToSprite(),
            "calm",
            1,
            "frost",
            new Color(1, 1, 1),
            [AbsentUtils.GetKeyword(Calm.Name)]
        ).GetComponentInChildren<TextMeshProUGUI>(true).enabled = true;

        Abduct.Data();

        StatusIconHelper.CreateIcon(
            "abduct",
            ImagePath("abduct.png").ToSprite(),
            "abduct",
            1,
            "frost",
            new Color(0.2f, 0.2f, 0.3f),
            [AbsentUtils.GetKeyword(Abduct.Name)]
        ).GetComponentInChildren<TextMeshProUGUI>(true).enabled = false;

        _assets =
        [
            /*
             * Status Effects
             */
            new StatusEffects.Ethereal().Builder(),
            new StatusEffects.Cat().Builder(),
            new StatusEffects.Calm().Builder(),
            new StatusEffects.Abduct().Builder(),

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


            /*
             * Keywords
             */
            new GoldRush().Builder(),
            new Rest().Builder(),
            new Trample().Builder(),
            new Keywords.Valor().Builder(),

            /*
             * Traits
             */
            new Traits.GoldRush().Builder(),
            new Traits.Rest().Builder(),
            new Traits.Trample().Builder(),
            new Valor().Builder(),

            /*
             * Cards (Companions)
             */
            new FrozenFlame().Builder(),
            new UnboundFlame().Builder(),

            new SalvoKitty().Builder(),
            new FussiladeCat().Builder(),

            new PanickedNut().Builder(),

            new Elsta().Builder(),

            new Lusine().Builder(),
            new Eudora().Builder(),

            new Blahaj().Builder(),
            new Aftonsparv().Builder(),
            new Blackfisk().Builder(),
            new Val().Builder(),
            new Kramig().Builder(),

            /*
             * Cards (Clunker)
             */

            /*
             * Cards (Items)
             */
            new Missile().Builder(),

            new NovaShard().Builder(),

            new BlingThrow().Builder(),

            new IceShard().Builder(),

            new Avarice().Builder(),

            new CursedClaymore().Builder(),

            new GhostlyPresence().Builder(),

            new Sarcophagus().Builder(),
            new DummyCardSarcophagus().Builder(),

            new RescueUFO().Builder(),

            /*
             * Card Upgrades
             */
            new MitosisCharm().Builder(),
            new ViolenceCharm().Builder(),
            new CursedCharm().Builder(),
            new CatCharm().Builder(),
            new BraveryButton().Builder(),
            new WillButton().Builder(),
            new FortitudeButton().Builder(),
            new ValorButton().Builder(),
            // new SarcophagusCharm().Builder()

            new SharkCharm().Builder()
        ];

        _loaded = true;
    }

    private void UnloadFromClasses()
    {
        var tribes = AddressableLoader.GetGroup<ClassData>("ClassData");
        foreach (var tribe in tribes)
        {
            if (tribe == null || tribe.rewardPools == null)
                continue;

            foreach (var pool in tribe.rewardPools)
            {
                if (pool == null)
                    continue;

                pool.list.RemoveAllWhere(item => item == null || item.ModAdded == this);
            }
        }
    }

    public override List<T> AddAssets<T, TY>()
    {
        if (_assets.OfType<T>().Any())
            LogHelper.Warn(
                $"[{Title}] adding {typeof(TY).Name}s: {_assets.OfType<T>().Select(a => a._data.name).Join()}");
        return _assets.OfType<T>().ToList();
    }

    private static string GetDescription()
    {
        return MakeDescription(
            "Avalanche of new and random cards and charms",
            "Lots of sillies to play with :3",
            "",
            "Content:",
            "11 Companions",
            "1 Pet",
            "6 Items",
            "8 Charms",
            "4 Status Effects",
            "4 Traits, one of which a targeting mode" +
            "+ More to come",
            "",
            "Additional Credits:",
            "MegaMarine: Sprites for Ethereal, Calm, and Cat",
            "Gaziter: Character designs for Lusine, and Eudora (and her buttons)",
            "Sunny: Sprites for Eudora",
            "The Wildfrost Discords modding section: Being helpful, giving suggestions, and feedback",
            "",
            "Source code: https://github.com/AbsentAbigail/AbsentAvalanche",
            "",
            "Extra note:",
            "This mod includes the companions from the Blahaj and Friends mod. The original Blahaj and Friends mod will not be updated anymore"
        );
    }

    private static string MakeDescription(params string[] lines)
    {
        return lines.Join(delimiter: "\n");
    }
}