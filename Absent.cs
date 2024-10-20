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
using TMPro;
using UnityEngine;
using WildfrostHopeMod.SFX;
using WildfrostHopeMod.Utils;
using WildfrostHopeMod.VFX;
using Cat = AbsentAvalanche.Keywords.Cat;
using Ethereal = AbsentAvalanche.Keywords.Ethereal;

namespace AbsentAvalanche;

public class Absent(string directory) : WildfrostMod(directory)
{
    private List<object> _assets;
    private bool _loaded;

    //this is here to allow our icon to appear in the text box of cards
    public TMP_SpriteAsset AssetSprites;

    public override string GUID => "absentabigail.wildfrost.absentavalanche";

    public override string[] Depends => ["hope.wildfrost.vfx", "absentabigail.wildfrost.absentutils"];

    public override string Title => "Absent Avalanche";

    public override string Description => "Avalanche of new and random cards and charms";
    public override TMP_SpriteAsset SpriteAsset => AssetSprites;

    public override void Load()
    {
        AbsentUtils.AddModInfo(new AbsentUtils.ModInfo
        {
            Mod = this,
            Prefix = "AbsentAvalanche"
        });

        if (!_loaded) CreateModAssets();
        base.Load();

        //needed for custom icons
        var floatingText = Object.FindObjectOfType<FloatingText>(true);
        floatingText.textAsset.spriteAsset.fallbackSpriteAssets.Add(AssetSprites);
    }

    public override void Unload()
    {
        UnloadFromClasses();
        base.Unload();
    }

    public void CreateModAssets()
    {
        //Needed to get sprites in text boxes
        AssetSprites =
            HopeUtils.CreateSpriteAsset("assetSprites", ImagePath(""), [], []);
        
        VFXHelper.VFX = new GIFLoader(this, ImagePath("Anim"));
        VFXHelper.VFX.RegisterAllAsApplyEffect();

        VFXHelper.SFX = new SFXLoader(ImagePath("Sounds"));
        VFXHelper.SFX.RegisterAllSoundsToGlobal();

        
        Ethereal.Data();

        //make sure you icon is in both the images folder and the sprites subfolder
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
        
        //make sure you icon is in both the images folder and the sprites subfolder
        StatusIconHelper.CreateIcon(
            "cat",
            ImagePath("cat.png").ToSprite(),
            "cat",
            1,
            "vim",
            new Color(0.2f, 0.2f, 0.3f),
            [AbsentUtils.GetKeyword(Cat.Name)]
        ).GetComponentInChildren<TextMeshProUGUI>(true).enabled = true;

        _assets =
        [
            /*
             * Status Effects
             */
            new StatusEffects.Ethereal().Builder(),
            new StatusEffects.Cat().Builder(),

            new OnCardPlayedGainOverload().Builder(),
            new WhenDestroyedSummonUnboundFlame().Builder(),
            new InstantSummonUnboundFlame().Builder(),
            new SummonUnboundFlame().Builder(),
            new OnCardPlayedApplyOverloadToAlliesInRow().Builder(),

            new TriggerAgainstTargetWhenMissileAttacks().Builder(),
            new OnCardPlayedKillSelf().Builder(),

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

            /*
             * Keywords
             */
            new GoldRush().Builder(),

            new Rest().Builder(),
            
            new Trample().Builder(),

            /*
             * Traits
             */
            new Traits.GoldRush().Builder(),

            new Traits.Rest().Builder(),
            
            new Traits.Trample().Builder(),

            /*
             * Cards (Companions)
             */
            new FrozenFlame().Builder(),
            new UnboundFlame().Builder(),

            new SalvoKitty().Builder(),

            new PanickedNut().Builder(),

            new Elsta().Builder(),
            
            new Lusine().Builder(),

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

            /*
             * Card Upgrades
             */
            new MitosisCharm().Builder(),
            new ViolenceCharm().Builder(),
            new CursedCharm().Builder()
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
}