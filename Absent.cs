using System.Collections.Generic;
using System.IO;
using System.Linq;
using AbsentAvalanche.Assets;
using AbsentAvalanche.Cards.Companion;
using AbsentAvalanche.Cards.Leaders;
using AbsentAvalanche.StatusEffects;
using AbsentAvalanche.StatusEffects.Implementations;
using AbsentUtilities;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.U2D;
using WildfrostHopeMod;
using WildfrostHopeMod.SFX;
using WildfrostHopeMod.Utils;
using WildfrostHopeMod.VFX;
using Abduct = AbsentAvalanche.Keywords.Abduct;
using Calm = AbsentAvalanche.Keywords.Calm;
using Cat = AbsentAvalanche.Keywords.Cat;
using Ethereal = AbsentAvalanche.Keywords.Ethereal;
using FakeCalm = AbsentAvalanche.Keywords.FakeCalm;
using Valor = AbsentAvalanche.Traits.Valor;

namespace AbsentAvalanche;

public class Absent : WildfrostMod
{
    public static Absent Instance;

    public static List<CardDataBuilder> Leaders;
    private static SpriteAtlas _spriteAtlas;
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

    public Absent(string directory) : base(directory)
    {
        Instance = this;
    }

    public override string GUID => "absentabigail.wildfrost.absentavalanche";

    public override string[] Depends =>
        ["hope.wildfrost.vfx", "hope.wildfrost.configs", "absentabigail.wildfrost.absentutils"];

    public override string Title => "Absent Avalanche";

    public override string Description =>
        "Avalanche of new and random cards and charms\n" +
        "Lots of sillies to play with :3";

    public override TMP_SpriteAsset SpriteAsset => _assetSprites;

    public static string CatalogFolder => Path.Combine(Instance.ModDirectory, "Windows");
    public static string CatalogPath => Path.Combine(CatalogFolder, "catalog.json");

    public override void Load()
    {
        StopWatch.Start();

        AbsentUtils.AddModInfo(new AbsentUtils.ModInfo
        {
            Mod = this,
            Prefix = "AbsentAvalanche"
        });

        if (!Addressables.ResourceLocators.Any(r => r is ResourceLocationMap map && map.LocatorId == CatalogPath))
            Addressables.LoadContentCatalogAsync(CatalogPath).WaitForCompletion();

        _spriteAtlas = (SpriteAtlas)Addressables.LoadAssetAsync<Object>($"Assets/{GUID}/Sprite Atlas.spriteatlas")
            .WaitForCompletion();
        AbsentUtils.GetModInfo().SetSprites(_spriteAtlas);

        var modInfo = AbsentUtils.GetModInfo();
        Debug.Log("Bundle sprite count: " + modInfo.Sprites?.spriteCount);

        VFXHelper.VFX = new GIFLoader(this, ImagePath("Anim"));
        VFXHelper.VFX.RegisterAllAsApplyEffect();

        VFXHelper.SFX = new SFXLoader(ImagePath("Sounds"));
        VFXHelper.SFX.RegisterAllSoundsToGlobal();

        LoadStatusIcons();

        if (!_loaded) CreateModAssets();
        base.Load();

        var l = Leaders.Select(builder => builder._data.name).ToArray();
        var tribes = AddressableLoader.GetGroup<ClassData>("ClassData");
        foreach (var pool in from tribe in tribes
                 where tribe != null && tribe.rewardPools != null
                 from pool in tribe.rewardPools
                 where pool != null
                 select pool)
            pool.list.RemoveAllWhere(data => l.Contains(data.name));

        var gameMode = AbsentUtils.TryGet<GameMode>("GameModeNormal"); //GameModeNormal is the standard game mode. 
        gameMode.classes = gameMode.classes.Append(AbsentUtils.TryGet<ClassData>(PlushTribe.Name)).ToArray();
        // gameMode.classes = gameMode.classes.Append(AbsentUtils.TryGet<ClassData>(NebulaTribe.Name)).ToArray();

        // Overrides
        AbsentUtils.GetKeyword("immunetosnow").show = true;
        AbsentUtils.GetKeyword("immunetosnow").showName = true;

        var aimless = AbsentUtils.GetTrait("Aimless");
        var barrage = AbsentUtils.GetTrait("Barrage");
        var longshot = AbsentUtils.GetTrait("Longshot");
        var valor = AbsentUtils.GetTrait(Valor.Name);
        aimless.overrides = aimless.overrides.AddToArray(valor);
        barrage.overrides = aimless.overrides.AddToArray(valor);
        longshot.overrides = aimless.overrides.AddToArray(valor);

        // Events
        LoadEvents();

        //needed for custom icons
        var floatingText = Object.FindObjectOfType<FloatingText>(true);
        floatingText.textAsset.spriteAsset.fallbackSpriteAssets.Add(_assetSprites);

        LogHelper.Log($"Loaded Absent Avalanche in {StopWatch.Stop()} ms");
    }

    private void LoadStatusIcons()
    {
        //Needed to get sprites in text boxes
        _assetSprites = HopeUtils.CreateSpriteAsset("AbsentAvalancheAssets", ImagePath("Icons"));

        Ethereal.Data();

        StatusIconHelper.CreateIcon(
            "ethereal",
            ImagePath("Icons/ethereal.png").ToSprite(),
            "ethereal",
            1,
            "vim",
            new Color(0.2f, 0.2f, 0.3f),
            [AbsentUtils.GetKeyword(Ethereal.Name)]
        ).GetComponentInChildren<TextMeshProUGUI>(true).enabled = true;

        Cat.Data();

        var catIcon = StatusIconHelper.CreateIcon(
            "cat",
            ImagePath("Icons/cat.png").ToSprite(),
            "cat",
            1,
            "ink",
            new Color(0.2f, 0.2f, 0.3f),
            [AbsentUtils.GetKeyword(Cat.Name)]
        );
        catIcon.GetComponentInChildren<TextMeshProUGUI>(true).enabled = true;
        catIcon.transform.Find("Text").Translate(-0.01f, -0.16f, 0);

        Calm.Data();

        StatusIconHelper.CreateIcon(
            "calm",
            ImagePath("Icons/calm.png").ToSprite(),
            "calm",
            1,
            "frost",
            new Color(1, 1, 1),
            [AbsentUtils.GetKeyword(Calm.Name)]
        ).GetComponentInChildren<TextMeshProUGUI>(true).enabled = true;

        FakeCalm.Data();

        StatusIconHelper.CreateIcon(
            "fakecalm",
            ImagePath("Icons/calm.png").ToSprite(),
            "fakecalm",
            1,
            "frost",
            new Color(1, 1, 1),
            [AbsentUtils.GetKeyword(Calm.Name)]
        ).GetComponentInChildren<TextMeshProUGUI>(true).enabled = true;

        Abduct.Data();

        StatusIconHelper.CreateIcon(
            "abduct",
            ImagePath("Icons/abduct.png").ToSprite(),
            "abduct",
            1,
            "frost",
            new Color(0.2f, 0.2f, 0.3f),
            [AbsentUtils.GetKeyword(Abduct.Name)]
        ).GetComponentInChildren<TextMeshProUGUI>(true).enabled = false;
    }

    public override void Unload()
    {
        UnloadEvents();
        base.Unload();

        var gameMode = AbsentUtils.TryGet<GameMode>("GameModeNormal");
        gameMode.classes =
            AbsentUtils.RemoveNulls(gameMode
                .classes); //Without this, a non-restarted game would crash on tribe selection
        AbsentUtils.UnloadFromClasses();

        var aimless = AbsentUtils.GetTrait("Aimless");
        var barrage = AbsentUtils.GetTrait("Barrage");
        var longshot = AbsentUtils.GetTrait("Longshot");
        aimless.overrides = AbsentUtils.RemoveNulls(aimless.overrides);
        barrage.overrides = AbsentUtils.RemoveNulls(barrage.overrides);
        longshot.overrides = AbsentUtils.RemoveNulls(longshot.overrides);

        AbsentUtils.GetKeyword("immunetosnow").show = false;
        AbsentUtils.GetKeyword("immunetosnow").showName = false;
    }

    private static void LoadEvents()
    {
        Events.OnCampaignGenerated += CampaignDataFix.SaveCampaignData;
        Events.OnCampaignLoaded += CampaignDataFix.LoadCampaignData;
        Events.OnEntityCreated += FixImage;
        Events.OnSceneLoaded += CombineCombos.SceneLoaded;
    }

    private static void UnloadEvents()
    {
        Events.OnCampaignGenerated -= CampaignDataFix.SaveCampaignData;
        Events.OnCampaignLoaded -= CampaignDataFix.LoadCampaignData;
        Events.OnEntityCreated -= FixImage;
        Events.OnSceneLoaded -= CombineCombos.SceneLoaded;
    }

    private void CreateModAssets()
    {
        /*
         * Tribes
         */
        _assets =
        [
            PlushTribe.Builder(),
            new StatusEffectDataBuilder(this)
                .Create<StatusEffectCamcorder>("Camcorder")
                .WithText("Camcorder")
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    var status = (StatusEffectCamcorder)data;
                    status.effectToApply = AbsentUtils.GetStatus("Scrap");
                    status.applyToFlags = StatusEffectApplyX.ApplyToFlags.Target;
                }),
            new StatusEffectDataBuilder(this)
                .Create<StatusEffectInstantDelegate>("instant func")
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    var status = (StatusEffectInstantDelegate)data;
                    status.Action = instant =>
                    {
                        var bell = Object.FindObjectOfType<RedrawBellSystem>();
                        var counterChange = bell.counterChange;
                        bell.counterChange = -instant.count;
                        bell.Counter();
                        bell.counterChange = counterChange;
                    };
                })
            // NebulaTribe.Builder()
        ];

        /*
         * Cards (Leaders)
         */
        Leaders =
        [
            new Leader<LilGuy>(-1, 3, -1, 2, -1).Builder(),
            new Leader<Jerry>(1, 2, -1, 2, -1).Builder(),
            new Leader<Alice>(-2, 1, -1, 2, -2).Builder(),
            new Leader<Seal>(-2, 2, -2, 2).Builder(),
            new Leader<Bamboozle>(-2, 1, counterModMin: -2, counterModMax: 1).Builder(),
            new Leader<May>(-1, 1, counterModMin: -1, counterModMax: 1).Builder(),
            new Leader<Sam>(-2, 3, -1, 1, -1, 1).Builder(),
            new Leader<Sherba>(-2, 3, counterModMin: -2, counterModMax: 1).Builder(),
            new Leader<Chirp>(-1, 1, -1, 1, -1, 1).Builder(),
            new Leader<Cuddles>(-1, 1, counterModMin: -1, counterModMax: 1).Builder(),
            new Leader<Bubbles>(-1, 2).Builder(),
            new Leader<Nami>(0, 1, 0, 2, -1).Builder(),
            new Leader<April>(2, 3, 0, 0, -1, 0, card =>
            {
                card.traits =
                [
                    .. card.traits,
                    AbsentUtils.TStack("Spark")
                ];
                card.startWithEffects =
                [
                    .. card.startWithEffects,
                    AbsentUtils.SStack(WhileActiveCountDownEtherealWhenDrawn.Name)
                ];
            }).Builder()
        ];
        _assets.AddRange(Leaders);

        AssetsCompanions.AddToAssets(_assets);
        AssetsClunkers.AddToAssets(_assets);
        AssetsItems.AddToAssets(_assets);
        AssetsCardUpgrades.AddToAssets(_assets);
        AssetsStatusEffects.AddToAssets(_assets);
        AssetsKeywords.AddToAssets(_assets);
        AssetsTraits.AddToAssets(_assets);
        AssetsFlavours.AddToAssets(_assets);

        CreateLocalizedStrings();

        _loaded = true;
    }

    private static void CreateLocalizedStrings()
    {
        var uiText = LocalizationHelper.GetCollection("UI Text", SystemLanguage.English);
        uiText.SetString(PlushTribe.TitleKey, "The Plushies");
        uiText.SetString(PlushTribe.DescKey,
            """
            Plush has found its way into the cold wilderness. Determined to restore the cozy warmth of the sun, they will valiantly oppose the Frost!

            Make sure to pet the plush when they do well, and don't let them get hurt! Plushies need proper love and care!
            """);

        uiText.SetString(NebulaTribe.TitleKey, "The Nebula");
        uiText.SetString(NebulaTribe.DescKey,
            """
            Weird space fog is covering the area... What could this be?

            No fight stays the same, none of your cards are certain
            """);

        uiText.SetString(InstantTutorDeck.Name, "Choose and draw a card from your draw pile");
        uiText.SetString(InstantTutorDiscard.Name, "Choose and draw a card from your discard pile");
        uiText.SetString(InstantTutorThreeRandomCompanions.Name, "Add a companion to your hand");
        uiText.SetString(InstantTutorThreeRandomTreasures.Name, "Add an item or a clunker to your hand");
        uiText.SetString(InstantTutorDeckCopyZoomlinConsume.Name,
            "Add a copy of a card in your deck to hand with zoomlin and consume");
        uiText.SetString(InstantTutorTenRandomCardsZoomlin.Name,
            "Add a random card to your hand with zoomlin and consume");
    }

    public override List<T> AddAssets<T, TY>()
    {
        if (_assets.OfType<T>().Any())
            LogHelper.Warn(
                $"Adding {typeof(TY).Name}s: {_assets.OfType<T>().Select(a => a._data.name).Join()}");
        return _assets.OfType<T>().ToList();
    }

    private static void FixImage(Entity entity)
    {
        if (entity.display is not Card { hasScriptableImage: false } card)
            return;
        card.mainImage.gameObject.SetActive(true);
    }
}