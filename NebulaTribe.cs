using System.Collections.Generic;
using System.Linq;
using AbsentAvalanche.Cards.Clunkers;
using AbsentAvalanche.Cards.Items;
using AbsentUtilities;
using Deadpan.Enums.Engine.Components.Modding;
using UnityEngine;

namespace AbsentAvalanche;

public static class NebulaTribe
{
    public const string Name = "NebulaTribe";
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
                playerCharacter.name = "AbsentAvalanche.Nebula";
                tribe.characterPrefab = playerCharacter.GetComponent<Character>();

                var inventory = ScriptableHelper.CreateScriptable<Inventory>("Inventory (AbsentAvalanche.NebulaTribe)");
                inventory.deck.list = DataList<CardData>(
                    Imagination.Name,
                    Imagination.Name,
                    Imagination.Name,
                    Imagination.Name,
                    Imagination.Name,
                    Imagination.Name,
                    Imagination.Name,
                    Imagination.Name,
                    Imagination.Name,
                    Imagination.Name
                ).ToList();
                tribe.startingInventory = inventory;

                tribe.leaders = DataList<CardData>(Absent.Leaders.Select(builder => builder._data.name).ToArray());

                tribe.rewardPools =
                [
                    UnitPool(),
                    ItemPool(),
                    Extensions.GetRewardPool("GeneralModifierPool"),
                    Extensions.GetRewardPool("GeneralCharmPool"),
                    Extensions.GetRewardPool("SnowCharmPool"),
                    Extensions.GetRewardPool("BasicCharmPool"),
                    Extensions.GetRewardPool("MagicCharmPool"),
                    Extensions.GetRewardPool("ClunkCharmPool")
                ];
            });
    }

    private static RewardPool UnitPool()
    {
        var cardData = AbsentUtils.GetCard(NebulaAuxilium.Name);
        List<CardData> list = [];
        for (var i = 0; i < 40; i++)
            list.Add(cardData.Clone());
        return TribeHelper.CreateRewardPool("NebulaCompanion", RewardPool.Type.Units.ToString(), [.. list]);
    }

    private static RewardPool ItemPool()
    {
        var cardData = AbsentUtils.GetCard(NebulaInstrumenta.Name);
        List<CardData> list = [];
        for (var i = 0; i < 80; i++)
            list.Add(cardData.Clone());
        return TribeHelper.CreateRewardPool("NebulaItem", RewardPool.Type.Items.ToString(),
            [.. list]);
    }

    private static T[] DataList<T>(params string[] names) where T : DataFile
    {
        return names.Select(s => AbsentUtils.TryGet<T>(s)).ToArray();
    }
}