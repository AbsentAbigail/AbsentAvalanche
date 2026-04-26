#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using AbsentAvalanche.Builders.Cards.Clunkers;
using AbsentAvalanche.Builders.Cards.Companions;
using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Builders.StatusEffects;
using AbsentAvalanche.Builders.Traits;
using AbsentAvalanche.Builders.Tribes;
using AbsentAvalanche.EventHooks;
using AbsentAvalanche.Helpers;
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
using Extensions = Deadpan.Enums.Engine.Components.Modding.Extensions;
using Object = UnityEngine.Object;

#endregion

namespace AbsentAvalanche;

public class Absent : WildfrostMod
{
    public static Absent Instance;
    private static SpriteAtlas _spriteAtlas;

    private List<object> _assets;

    //this is here to allow our icon to appear in the text box of cards
    public override TMP_SpriteAsset SpriteAsset => _assetSprites;
    private TMP_SpriteAsset _assetSprites;
    
    private bool _loaded;

    public Absent(string directory) : base(directory)
    {
        Instance = this;
    }

    public static string CatalogFolder => Path.Combine(Instance.ModDirectory, "Windows");
    public static string CatalogPath => Path.Combine(CatalogFolder, "catalog.json");

    public override string GUID => "absentabigail.wildfrost.absentavalanche";

    public override string[] Depends =>
        ["hope.wildfrost.vfx", "hope.wildfrost.configs"];

    public override string Title => "Absent Avalanche";

    public override string Description => """
                                          Avalanche of new and random cards and charms
                                          Lots of sillies to play with :3
                                          """;

    // Configuration properties
    
    [ConfigInput] [ConfigSlider(0f, 10f)] [ConfigItem(4f, null, "Val max size multiplier")] [UsedImplicitly]
    public float valMaxSize;

    [ConfigInput] [ConfigSlider(0, 100)] [ConfigItem(40, null, "Attack needed for max Val size")] [UsedImplicitly]
    public int valMaxSizeAt;

    [ConfigInput] [ConfigSlider(0f, 10f)] [ConfigItem(0.25f, null, "Val min size multiplier")] [UsedImplicitly]
    public float valMinSize;

    
    public override void Load()
    {
        StopWatch.Start();

        if (!Addressables.ResourceLocators.Any(r => r is ResourceLocationMap map && map.LocatorId == CatalogPath))
            Addressables.LoadContentCatalogAsync(CatalogPath).WaitForCompletion();

        _spriteAtlas = (SpriteAtlas)Addressables.LoadAssetAsync<Object>($"Assets/{GUID}/Sprite Atlas.spriteatlas")
            .WaitForCompletion();

        VFXHelper.VFX = new GIFLoader(this, ImagePath("Anim"));
        VFXHelper.VFX.RegisterAllAsApplyEffect();

        VFXHelper.SFX = new SFXLoader(ImagePath("Sounds"));
        VFXHelper.SFX.RegisterAllSoundsToGlobal();

        //Needed to get sprites in text boxes
        _assetSprites = HopeUtils.CreateSpriteAsset("AbsentAvalancheAssets", ImagePath("Icons"));
        // IconKeywords.CreateIconKeywords();
        SpriteAsset.RegisterSpriteAsset();

        if (!_loaded) CreateModAssets();
        base.Load();

        LoadTribes();
        LoadOverrides();
        LoadEvents();
        
        LogHelper.Log($"Loaded Absent Avalanche in {StopWatch.Stop()} ms");
    }

    public override void Unload()
    {
        UnloadEvents();
        SpriteAsset.UnRegisterSpriteAsset();
        base.Unload();

        var gameMode = TryGet<GameMode>("GameModeNormal");
        gameMode.classes =
            RemoveNulls(gameMode
                .classes); //Without this, a non-restarted game would crash on tribe selection
        UnloadFromClasses();
        UnloadOverrides();
    }

    private void CreateModAssets()
    {
        _assets = [];

        _assets.AddRange([
            .. DreamTeam.EffectBuilders(Bubbles.Name, Cuddles.Name), // Bubbles and Cuddles
            .. DreamTeam.EffectBuilders(Bam.Name, Boozle.Name), // Bam and Boozle
            .. DreamTeam.EffectBuilders(Catcus.Name, Catcitten.Name), // Catci
            .. DreamTeam.EffectBuilders(Sherba.Name, Cuddles.Name), // Snuggle Buddies
            .. DreamTeam.EffectBuilders(Alice.Name, Nami.Name), // The coziest pacas
            .. DreamTeam.EffectBuilders(April.Name, May.Name, // Sheep and Dino
                instant => {
                    instant.replaceEffects =
                    [
                        [SStack(OnCardPlayedAddWoolGrenadeToHand.Name),
                        SStack(OnCardPlayedAddGoolWrenadeToHand.Name)]
                    ];
                }),
            .. DreamTeam.EffectBuilders(Bubbles.Name, Kiki.Name),
            .. DreamTeam.EffectBuilders(Emerald.Name, Sally.Name)
        ]);
        
        _assets.AddRange(Assembly.GetExecutingAssembly().GetTypes()
            .Where(t =>
                string.Equals(t.Namespace, "AbsentAvalanche.Builders.Icons",
                    StringComparison.Ordinal)
                && typeof(IIconBuilder).IsAssignableFrom(t))
            .Select(type => ((IIconBuilder)Activator.CreateInstance(type)).Builder()).ToList()
        );

        _assets.AddRange(Assembly.GetExecutingAssembly().GetTypes()
            .Where(t =>
                string.Equals(t.Namespace, "AbsentAvalanche.Builders.StatusEffects",
                    StringComparison.Ordinal)
                && typeof(IStatusBuilder).IsAssignableFrom(t))
            .Select(type => ((IStatusBuilder)Activator.CreateInstance(type)).Builder()).ToList()
        );
        
        _assets.AddRange(Assembly.GetExecutingAssembly().GetTypes()
            .Where(t =>
                string.Equals(t.Namespace, "AbsentAvalanche.Builders.Traits",
                    StringComparison.Ordinal)
                && typeof(ITraitBuilder).IsAssignableFrom(t))
            .Select(type => ((ITraitBuilder)Activator.CreateInstance(type)).Builder()).ToList()
        );
        
        _assets.AddRange(Assembly.GetExecutingAssembly().GetTypes()
            .Where(t =>
                string.Equals(t.Namespace, "AbsentAvalanche.Builders.Keywords",
                    StringComparison.Ordinal)
                && typeof(IKeywordBuilder).IsAssignableFrom(t))
            .Select(type => ((IKeywordBuilder)Activator.CreateInstance(type)).Builder()).ToList()
        );

        _assets.AddRange(Assembly.GetExecutingAssembly().GetTypes()
            .Where(t =>
                string.Equals(t.Namespace, "AbsentAvalanche.Builders.Flavours",
                    StringComparison.Ordinal)
                && typeof(IKeywordBuilder).IsAssignableFrom(t))
            .Select(type => ((IKeywordBuilder)Activator.CreateInstance(type)).Builder()).ToList()
        );

        _assets.AddRange(Assembly.GetExecutingAssembly().GetTypes()
            .Where(t =>
                string.Equals(t.Namespace, "AbsentAvalanche.Builders.Upgrades",
                    StringComparison.Ordinal)
                && typeof(IUpgradeBuilder).IsAssignableFrom(t))
            .Select(type => ((IUpgradeBuilder)Activator.CreateInstance(type)).Builder()).ToList()
        );

        _assets.AddRange(Assembly.GetExecutingAssembly().GetTypes()
            .Where(t =>
                string.Equals(t.Namespace, "AbsentAvalanche.Builders.Cards.Companions",
                    StringComparison.Ordinal)
                && typeof(ICardBuilder).IsAssignableFrom(t))
            .Select(type => (ICardBuilder)Activator.CreateInstance(type))
            .Where(builder => builder is not ILeaderBuilder { LeaderExclusive: true })
            .Select(builder => builder.Builder()).ToList()
        );

        _assets.AddRange(Assembly.GetExecutingAssembly().GetTypes()
            .Where(t =>
                string.Equals(t.Namespace, "AbsentAvalanche.Builders.Cards.Companions",
                    StringComparison.Ordinal)
                && typeof(ILeaderBuilder).IsAssignableFrom(t))
            .Select(type =>
            {
                var builder = (ILeaderBuilder)Activator.CreateInstance(type);
                var mods = builder.LeaderModifiers;
                var cardBuilder = (CardDataBuilder)builder.Builder();
                cardBuilder
                    .WithCardType("Leader")
                    .FreeModify(card =>
                    {
                        card.name += "Leader";
                        card.createScripts =
                        [
                            LeaderHelper.GiveUpgrade(),
                            LeaderHelper.AddRandomHealth(mods.healthRange),
                            LeaderHelper.AddRandomDamage(mods.damageRange),
                            LeaderHelper.AddRandomCounter(mods.counterRange)
                        ];
                    })
                    .SubscribeToAfterAllBuildEvent(mods.subscribe.Invoke);
                
                return cardBuilder;
            }).ToList()
        );
        
        _assets.AddRange(Assembly.GetExecutingAssembly().GetTypes()
            .Where(t =>
                string.Equals(t.Namespace, "AbsentAvalanche.Builders.Cards.Items",
                    StringComparison.Ordinal)
                && typeof(ICardBuilder).IsAssignableFrom(t))
            .Select(type => ((ICardBuilder)Activator.CreateInstance(type)).Builder()).ToList()
        );

        _assets.AddRange(Assembly.GetExecutingAssembly().GetTypes()
            .Where(t =>
                string.Equals(t.Namespace, "AbsentAvalanche.Builders.Cards.Clunkers",
                    StringComparison.Ordinal)
                && typeof(ICardBuilder).IsAssignableFrom(t))
            .Select(type => ((ICardBuilder)Activator.CreateInstance(type)).Builder()).ToList()
        );

        _assets.AddRange(Assembly.GetExecutingAssembly().GetTypes()
            .Where(t =>
                string.Equals(t.Namespace, "AbsentAvalanche.Builders.Tribes",
                    StringComparison.Ordinal)
                && typeof(IClassBuilder).IsAssignableFrom(t))
            .Select(type => ((IClassBuilder)Activator.CreateInstance(type)).Builder()).ToList());

        _assets.AddRange(Assembly.GetExecutingAssembly().GetTypes()
            .Where(t =>
                string.Equals(t.Namespace, "AbsentAvalanche.Builders.GameModifiers",
                    StringComparison.Ordinal)
                && typeof(IGameModifierBuilder).IsAssignableFrom(t))
            .Select(type => ((IGameModifierBuilder)Activator.CreateInstance(type)).Builder()).ToList());
        
        CreateLocalizedStrings();
        
        _loaded = true;
    }

    private static void LoadTribes()
    {
        var gameMode = TryGet<GameMode>("GameModeNormal"); //GameModeNormal is the standard game mode. 
        gameMode.classes = gameMode.classes.Append(GetTribe(PlushTribe.Name)).ToArray();
        // gameMode.classes = gameMode.classes.Append(GetTribe(NebulaTribe.Name)).ToArray();
    }

    private static void LoadOverrides()
    {
        GetKeyword("immunetosnow").show = true;
        GetKeyword("immunetosnow").showName = true;

        var aimless = GetTrait("Aimless");
        var barrage = GetTrait("Barrage");
        var longshot = GetTrait("Longshot");
        var valor = GetTrait(Valor.Name);
        aimless.overrides = aimless.overrides.AddToArray(valor);
        barrage.overrides = aimless.overrides.AddToArray(valor);
        longshot.overrides = aimless.overrides.AddToArray(valor);

        var split = GetStatusOf<StatusEffectInstantSplit>("Split");
        var longCatProfile = new StatusEffectInstantSplit.Profile
        {
            cardName = GetCard(LongCat.Name).name,
            changeToCardName = GetCard(LongKitty.Name).name
        };

        var longCatLeaderProfile = new StatusEffectInstantSplit.Profile
        {
            cardName = GetCard(LongCat.Name + "Leader").name,
            changeToCardName = GetCard(LongKitty.Name + "Leader").name
        };
        split.profiles = [
            .. split.profiles,
            longCatProfile,
            longCatLeaderProfile
        ];

        var spice = GetStatus("Spice");
        spice.targetConstraints =
        [
            TargetConstraintHelper.Or(
                "Does Damage OR Has Heating",
                false,
                TargetConstraintHelper.General<TargetConstraintDoesDamage>("Does Damage"),
                TargetConstraintHelper.HasTrait(Heating.Name)
            )
        ];
        var demonize = GetStatus("Demonize");
        demonize.targetConstraints =
        [
            TargetConstraintHelper.Or(
                "Demonize constrains or equipment",
                false,
                [TargetConstraintHelper.HasStatus(Equip.Name), .. demonize.targetConstraints]
            )
        ];
    }

    private static void UnloadOverrides()
    {
        var aimless = GetTrait("Aimless");
        var barrage = GetTrait("Barrage");
        var longshot = GetTrait("Longshot");
        aimless.overrides = RemoveNulls(aimless.overrides);
        barrage.overrides = RemoveNulls(barrage.overrides);
        longshot.overrides = RemoveNulls(longshot.overrides);

        GetKeyword("immunetosnow").show = false;
        GetKeyword("immunetosnow").showName = false;
        
        var spice = GetStatus("Spice");
        spice.targetConstraints = [TargetConstraintHelper.General<TargetConstraintDoesDamage>("Does Damage")];
        var demonize = GetStatus("Demonize");
        demonize.targetConstraints = [TargetConstraintHelper.General<TargetConstraintCanBeHit>()];
    }
    
    private static void LoadEvents()
    {
        Events.OnCampaignGenerated += CampaignDataFix.SaveCampaignData;
        Events.OnCampaignLoaded += CampaignDataFix.LoadCampaignData;
        Events.OnEntityCreated += LeaderImageFix.FixImage;
        Events.OnSceneLoaded += CombineCombos.SceneLoaded;
        Events.OnPreCampaignPopulate += NestReplace.Replace;
        Events.OnCampaignGenerated += LuminBlessing.ResetLuminBlessing;
    }

    private static void UnloadEvents()
    {
        Events.OnCampaignGenerated -= CampaignDataFix.SaveCampaignData;
        Events.OnCampaignLoaded -= CampaignDataFix.LoadCampaignData;
        Events.OnEntityCreated -= LeaderImageFix.FixImage;
        Events.OnSceneLoaded -= CombineCombos.SceneLoaded;
        Events.OnPreCampaignPopulate -= NestReplace.Replace;
        Events.OnCampaignGenerated -= LuminBlessing.ResetLuminBlessing;
    }

    private void UnloadFromClasses()
    {
        // Remove data from Tribes
        var tribes = AddressableLoader.GetGroup<ClassData>("ClassData");
        foreach (var pool in from tribe in tribes
                 where tribe != null && tribe.rewardPools != null
                 from pool in tribe.rewardPools
                 where pool != null
                 select pool) pool.list.RemoveAllWhere(item => item == null || item.ModAdded == this);
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

//         uiText.SetString(NebulaTribe.TitleKey, "The Nebula");
//         uiText.SetString(NebulaTribe.DescKey,
//             """
//             Weird space fog is covering the area... What could this be?
//
//             No fight stays the same, none of your cards are certain
//             """);

        uiText.SetString(InstantTutorDeck.Name, "Choose and draw a card from your draw pile");
        uiText.SetString(InstantTutorDiscard.Name, "Choose and draw a card from your discard pile");
        uiText.SetString(InstantTutorThreeRandomCompanions.Name, "Add a companion to your hand");
        uiText.SetString(InstantTutorThreeRandomTreasures.Name, "Add an item or a clunker to your hand");
        uiText.SetString(InstantTutorDeckCopyZoomlinConsume.Name,
            "Add a copy of a card in your deck to hand with zoomlin and consume");
        uiText.SetString(InstantTutorTenRandomCardsZoomlin.Name,
            "Add a random card to your hand with zoomlin");
    }

    public override List<T> AddAssets<T, TY>()
    {
        if (_assets.OfType<T>().Any())
            LogHelper.Warn($"[{Title}] adding {typeof(TY).Name}s: {_assets.OfType<T>().Select(a => a._data.name).Join()}");
        return _assets.OfType<T>().ToList();
    }

    public static T[] RemoveNulls<T>(T[] data) where T : DataFile
    {
        var list = data.ToList();
        list.RemoveAll(x => x == null || x.ModAdded == Instance);
        return list.ToArray();
    }

    public static Sprite GetSprite(string spriteName)
    {
        return _spriteAtlas.GetSprite(spriteName) ?? Instance.ImagePath($"{spriteName}.png").ToSprite();
    }

    // Bells not bundled because of size and offset changes
    public static Sprite GetBellSprite(string spriteName, float offset)
    {
        var texture = Instance.ImagePath(Path.Combine("Bells", spriteName + ".png")).ToTex();
        return Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, offset), 327f);;
    }
    
    public static string PrefixGuid(string name)
    {
        return Extensions.PrefixGUID(name, Instance);
    }

    public static StatusEffectData GetStatus(string statusName)
    {
        return TryGet<StatusEffectData>(statusName);
    }

    public static CardData.StatusEffectStacks SStack(string statusName, int amount = 1)
    {
        return new CardData.StatusEffectStacks(
            GetStatus(statusName),
            amount);
    }

    public static CardData.TraitStacks TStack(string traitName, int amount = 1)
    {
        return new CardData.TraitStacks(
            GetTrait(traitName),
            amount);
    }

    public static T GetStatusOf<T>(string statusName) where T : StatusEffectData
    {
        return TryGet<T>(statusName);
    }

    public static CardData GetCard(string cardName)
    {
        return TryGet<CardData>(cardName);
    }

    public static CardUpgradeData GetCardUpgrade(string cardUpgradeName)
    {
        return TryGet<CardUpgradeData>(cardUpgradeName);
    }

    public static TraitData GetTrait(string traitName)
    {
        return TryGet<TraitData>(traitName);
    }

    public static KeywordData GetKeyword(string keywordName)
    {
        return TryGet<KeywordData>(keywordName);
    }

    public static ClassData GetTribe(string tribeName)
    {
        return TryGet<ClassData>(tribeName);
    }

    public static CardType GetCardType(string cardTypeName)
    {
        return TryGet<CardType>(cardTypeName);
    }

    public static T TryGet<T>(string datafileName) where T : DataFile
    {
        T dataFile;
        if (typeof(StatusEffectData).IsAssignableFrom(typeof(T)))
            dataFile = Instance.Get<StatusEffectData>(datafileName) as T;
        else
            dataFile = Instance.Get<T>(datafileName);

        return dataFile ??
               throw new Exception(
                   $"TryGet Error: Could not find a [{typeof(T).Name}] with the name [{datafileName}] or [{Extensions.PrefixGUID(datafileName, Instance)}]");
    }

    public static StatusEffectDataBuilder StatusCopy(string oldName, string newName)
    {
        var data = GetStatus(oldName).InstantiateKeepName();
        data.name = PrefixGuid(newName);
        var builder = data.Edit<StatusEffectData, StatusEffectDataBuilder>();
        builder.Mod = Instance;
        return builder;
    }

    public static ClassDataBuilder TribeCopy(string oldName, string newName)
    {
        var data = GetTribe(oldName).InstantiateKeepName();
        data.name = PrefixGuid(newName);
        var builder = data.Edit<ClassData, ClassDataBuilder>();
        builder.Mod = Instance;
        return builder;
    }

    public static string CardTag(string name)
    {
        return $"<card={PrefixGuid(name)}>";
    }

    public static string VanillaCardTag(string name)
    {
        return $"<card={name}>";
    }

    public static string KeywordTag(string name)
    {
        return $"<keyword={PrefixGuid(name)}>";
    }
    
    public static string VanillaKeywordTag(string name)
    {
        return $"<keyword={name}>";
    }

    public static void AddToModifierPool(GameModifierData data)
    {
        Extensions.GetRewardPool("GeneralModifierPool").list.Add(data);
    }
}