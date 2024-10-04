using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;

namespace AbsentAvalanche.Helpers
{
    internal class CardUpgradeHelper
    {
        public static CardUpgradeDataBuilder Charm(string name, string title, string text, Pools pool = Pools.General)
        {
            return new CardUpgradeDataBuilder(Absent.Instance)
                .Create(name)
                .WithType(CardUpgradeData.Type.Charm)
                .WithImage($"{name}.png")
                .WithTitle(title)
                .WithText(text)
                .WithPools(PoolsToStringArray(pool));
        }

        public static string[] PoolsToStringArray(Pools pool)
        {
            string[] pools = [];

            if (pool.HasFlag(Helpers.Pools.General))
                pools = pools.AddToArray("GeneralCharmPool");

            if (pool.HasFlag(Helpers.Pools.Snowdweller))
                pools = pools.AddToArray("BasicCharmPool");

            if (pool.HasFlag(Helpers.Pools.Shademancer))
                pools = pools.AddToArray("MagicCharmPool");

            if (pool.HasFlag(Helpers.Pools.Clunkmaster))
                pools = pools.AddToArray("ClunkCharmPool");

            return pools;
        }
    }
}