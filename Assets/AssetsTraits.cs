using System.Collections.Generic;
using AbsentAvalanche.Traits;

namespace AbsentAvalanche.Assets;

public static class AssetsTraits
{
    public static void AddToAssets(List<object> assets)
    {
        assets.AddRange([
            new GoldRush().Builder(),
            new Rest().Builder(),
            new Trample().Builder(),
            new Valor().Builder(),
            new Scavenge().Builder(),
            new Warm().Builder()
        ]);
    }
}