using System.Collections.Generic;
using AbsentAvalanche.Keywords;

namespace AbsentAvalanche.Assets;

public static class AssetsKeywords
{
    public static void AddToAssets(List<object> assets)
    {
        assets.AddRange([
            new GoldRush().Builder(),
            new Rest().Builder(),
            new Trample().Builder(),
            new Valor().Builder(),
            new Sarcophagus().Builder(),
            new Scavenge().Builder(),
            new Royal().Builder(),
            new Warm().Builder(),
            new Panic().Builder()
        ]);
    }
}