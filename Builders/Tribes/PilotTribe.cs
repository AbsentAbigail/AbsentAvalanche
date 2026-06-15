using System;
using System.Linq;
using System.Reflection;
using AbsentAvalanche.Builders.Cards.Items;
using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Helpers;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using Extensions = Deadpan.Enums.Engine.Components.Modding.Extensions;
using Object = UnityEngine.Object;

namespace AbsentAvalanche.Builders.Tribes;

[UsedImplicitly]
public class PilotTribe : IClassBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public const string UnitPoolName = "Absent.PilotUnitPool";
    public const string ItemPoolName = "Absent.PilotItemPool";
    public const string CharmPoolName = "Absent.PilotCharmPool";
    public static readonly string TitleKey = Absent.PrefixGuid("PilotTribeTitle");
    public static readonly string DescKey = Absent.PrefixGuid("PilotTribeDesc");
    
    public DataFileBuilder<ClassData, ClassDataBuilder> Builder()
    {
        return Absent.TribeCopy("Basic", Name)
            .WithFlag(Absent.GetSprite("pilotbanner"))
            .SubscribeToAfterAllBuildEvent(tribe =>
            {
                tribe.id = "Pilot";
                
                var playerCharacter = tribe.characterPrefab.gameObject.InstantiateKeepName();
                Object.DontDestroyOnLoad(playerCharacter);
                playerCharacter.name = "AbsentAvalanche.Pilot";
                tribe.characterPrefab = playerCharacter.GetComponent<Character>();

                Inventory inventory = new Script<Inventory>("Inventory (AbsentAvalanche.PilotTribe)", null);
                inventory.deck.list = DataList<CardData>(
                    ButterflyKnife.Name, ButterflyKnife.Name, ButterflyKnife.Name, ButterflyKnife.Name,
                    Coolant.Name, Coolant.Name,
                    JumpStart.Name,
                    Cannon.Name,
                    "ZoomlinNest"
                ).ToList();
                tribe.startingInventory = inventory;

                tribe.leaders = GetLeaders();

                tribe.rewardPools =
                [
                    PlushTribe.UnitPool(),
                    PlushTribe.ItemPool(),
                    PlushTribe.CharmPool(),
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
                string.Equals(t.Namespace, "AbsentAvalanche.Builders.Cards.PilotLeaders",
                    StringComparison.Ordinal)
                && typeof(ILeaderBuilder).IsAssignableFrom(t))
            .Select(type => (ICardBuilder)Activator.CreateInstance(type))
            .Where(builder => builder is ILeaderBuilder { InPool: true })
            .Select(builder => builder.Builder()._data.name).ToArray();
        return DataList<CardData>(leaderNames);
    }
    
    private static T[] DataList<T>(params string[] names) where T : DataFile
    {
        return names.Select(Absent.TryGet<T>).ToArray();
    }
}