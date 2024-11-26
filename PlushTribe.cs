using System.Collections.Generic;
using System.Linq;
using AbsentAvalanche.Cards.Clunkers;
using AbsentAvalanche.Cards.Companion;
using AbsentAvalanche.Cards.Items;
using AbsentUtilities;
using Deadpan.Enums.Engine.Components.Modding;
using UnityEngine;
using PanickedNut = AbsentAvalanche.Keywords.flavour.PanickedNut;

namespace AbsentAvalanche;

public static class PlushTribe
{
    public const string Name = "PlushTribe";
    public const string UnitPoolName = "Absent.PlushieUnitPool";
    public const string ItemPoolName = "Absent.PlushieItemPool";
    public const string CharmPoolName = "Absent.PlushieCharmPool";
    public static readonly string TitleKey = AbsentUtils.PrefixGuid(".TribeTitle");
    public static readonly string DescKey = AbsentUtils.PrefixGuid(".TribeDesc");

    public static ClassDataBuilder Builder()
    {
        return AbsentUtils.TribeCopy("Basic", Name)
            .WithFlag("Images/plushbanner.png")
            .SubscribeToAfterAllBuildEvent(tribe =>
            {
                var playerCharacter = tribe.characterPrefab.gameObject.InstantiateKeepName();
                Object.DontDestroyOnLoad(playerCharacter);
                playerCharacter.name = "AbsentAvalanche.Plush";
                tribe.characterPrefab = playerCharacter.GetComponent<Character>();

                var inventory = ScriptableHelper.CreateScriptable<Inventory>("Inventory (AbsentAvalanche.PlushTribe)");
                inventory.deck.list = DataList<CardData>(
                    CatToy.Name, CatToy.Name, CatToy.Name, CatToy.Name,
                    Snowball.Name,
                    Blanket.Name, Blanket.Name,
                    Headpat.Name,
                    ShadyBox.Name,
                    PillowFortress.Name
                ).ToList();
                tribe.startingInventory = inventory;

                tribe.leaders = DataList<CardData>(Absent.Leaders.Select(builder => builder._data.name).ToArray());

                tribe.rewardPools =
                [
                    UnitPool(),
                    ItemPool(),
                    CharmPool(),
                    Extensions.GetRewardPool("GeneralModifierPool"),
                    Extensions.GetRewardPool("GeneralUnitPool"),
                    Extensions.GetRewardPool("GeneralItemPool"),
                    Extensions.GetRewardPool("GeneralCharmPool"),
                    Extensions.GetRewardPool("SnowUnitPool"),
                    Extensions.GetRewardPool("SnowItemPool"),
                    Extensions.GetRewardPool("SnowCharmPool")
                ];
            });
    }

    public static RewardPool UnitPool()
    {
        return TribeHelper.CreateRewardPool(UnitPoolName, RewardPool.Type.Units.ToString(),
            [.. DataList<CardData>(
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
                    Cards.Companion.PanickedNut.Name
                )]);
    }

    public static RewardPool ItemPool()
    {
        return TribeHelper.CreateRewardPool(ItemPoolName, RewardPool.Type.Items.ToString(),
            [.. DataList<CardData>(
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
                    GhostlyPresence.Name
                )]);
    }
    
    public static RewardPool CharmPool()
    {
        return TribeHelper.CreateRewardPool(CharmPoolName, RewardPool.Type.Charms.ToString(),
            [.. DataList<CardUpgradeData>(
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
                )]);
    }

    private static T[] DataList<T>(params string[] names) where T : DataFile
    {
        return names.Select(s => AbsentUtils.TryGet<T>(s)).ToArray();
    }
}