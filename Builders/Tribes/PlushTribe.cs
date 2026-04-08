#region

using System;
using System.Linq;
using System.Reflection;
using AbsentAvalanche.Builders.Cards.Clunkers;
using AbsentAvalanche.Builders.Cards.Companions;
using AbsentAvalanche.Builders.Cards.Items;
using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Helpers;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using UnityEngine;
using Object = UnityEngine.Object;

#endregion

namespace AbsentAvalanche.Builders.Tribes;

[UsedImplicitly]
public class PlushTribe : IClassBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public const string UnitPoolName = "Absent.PlushieUnitPool";
    public const string ItemPoolName = "Absent.PlushieItemPool";
    public const string CharmPoolName = "Absent.PlushieCharmPool";
    public static readonly string TitleKey = Absent.PrefixGuid("PlushTribeTitle");
    public static readonly string DescKey = Absent.PrefixGuid("PlushTribeDesc");
    
    public DataFileBuilder<ClassData, ClassDataBuilder> Builder()
    {
        return Absent.TribeCopy("Basic", Name)
            .WithFlag(Absent.GetSprite("plushbanner"))
            .SubscribeToAfterAllBuildEvent(tribe =>
            {
                var playerCharacter = tribe.characterPrefab.gameObject.InstantiateKeepName();
                Object.DontDestroyOnLoad(playerCharacter);
                playerCharacter.name = "AbsentAvalanche.Plush";
                tribe.characterPrefab = playerCharacter.GetComponent<Character>();

                Inventory inventory = new Script<Inventory>("Inventory (AbsentAvalanche.PlushTribe)", null);
                inventory.deck.list = DataList<CardData>(
                    CatToy.Name, CatToy.Name, CatToy.Name, CatToy.Name,
                    Snowball.Name,
                    Blanket.Name, Blanket.Name,
                    Headpat.Name,
                    ForgottenBox.Name,
                    PillowFortress.Name
                ).ToList();
                tribe.startingInventory = inventory;

                tribe.leaders = GetLeaders();

                tribe.rewardPools =
                [
                    UnitPool(),
                    ItemPool(),
                    CharmPool(),
                    Extensions.GetRewardPool("GeneralModifierPool"),
                    Extensions.GetRewardPool("GeneralUnitPool"),
                    Extensions.GetRewardPool("GeneralItemPool"),
                    Extensions.GetRewardPool("GeneralCharmPool"),
                ];
            });
    }

    private static CardData[] GetLeaders()
    {
        var leaderNames = Assembly.GetExecutingAssembly().GetTypes()
            .Where(t =>
                string.Equals(t.Namespace, "AbsentAvalanche.Builders.Cards.Companions",
                    StringComparison.Ordinal)
                && typeof(ILeaderBuilder).IsAssignableFrom(t))
            .Select(type => (ICardBuilder)Activator.CreateInstance(type))
            .Where(builder => builder is ILeaderBuilder { InPool: true })
            .Select(builder => builder.Builder()._data.name + "Leader").ToArray();
        
        // Remove leaders from pools
        var tribes = AddressableLoader.GetGroup<ClassData>("ClassData");
        foreach (var pool in from tribe in tribes
                 where tribe != null && tribe.rewardPools != null
                 from pool in tribe.rewardPools
                 where pool != null
                 select pool)
            pool.list.RemoveAllWhere(data => leaderNames.Contains(data.name));
        
        return DataList<CardData>(leaderNames);
    }
    
    private static RewardPool UnitPool()
    {
        return CreateRewardPool(UnitPoolName, RewardPool.Type.Units.ToString(),
        [
            .. DataList<CardData>(
                "BloodBoy", // Berry Sis
                "Turmeep", // Alloy
                "Witch",
                "TailsFive", // Chikichi
                "Egg",
                "Firefist",
                "Kernel",
                "LilBerry",
                "GuardianGnome", // Nom and Stompy
                "Pootie",
                "Pyra",
                "Shelly",
                "Kokonut", // Taiga
                "Tusk",
                "Zula",
                FrozenFlame.Name,
                PanickedNut.Name,
                Splash.Name
            )
        ]);
    }

    private static RewardPool ItemPool()
    {
        return CreateRewardPool(ItemPoolName, RewardPool.Type.Items.ToString(),
        [
            .. DataList<CardData>(
                "PomDispenser", // Gacha Pomper
                "Heartforge",
                "MobileCampfire",
                "PepperFlag",
                "SpiceSparklers",
                "Madness", // Sunglass Chime
                "ZoomlinNest",
                "BeepopMask",
                "Bumblebee", // Blaze Bom
                "Shwooper",
                "EnergyDart", // Clockwork Bom
                "DragonflamePepper",
                "FallowMask",
                "Recycler", // Forging Stove
                "Junberry", // Gigis Cookie Box
                "JunjunMask",
                "LuminShard", // Lumin Lantern
                "NutshellCake",
                "Peppereaper",
                "Peppering",
                "Putty",
                "ShellShield",
                "Shellbo",
                "SpiceStones",
                Catbom.Name,
                GhostlyPresence.Name,
                FireSpell.Name
            )
        ]);
    }

    private static RewardPool CharmPool()
    {
        return CreateRewardPool(CharmPoolName, RewardPool.Type.Charms.ToString(),
        [
            .. DataList<CardUpgradeData>(
                "CardUpgradeAcorn",
                "CardUpgradeSpiky",
                "CardUpgradeBom",
                "CardUpgradeConsumeOverload",
                "CardUpgradeOverload",
                "CardUpgradeTrash",
                "CardUpgradeHeartburn",
                "CardUpgradeShellBecomesSpice",
                "CardUpgradeScrap",
                "CardUpgradeShellOnKill",
                "CardUpgradeSpice",
                "CardUpgradeTeethWhenHit"
            )
        ]);
    }

    private static T[] DataList<T>(params string[] names) where T : DataFile
    {
        return names.Select(Absent.TryGet<T>).ToArray();
    }
    
    public static RewardPool CreateRewardPool(string name, string type, DataFile[] list)
    {
        var pool = ScriptableObject.CreateInstance<RewardPool>();
        pool.name = name;
        pool.type = type;
        pool.list = list.ToList();
        return pool;
    }
}