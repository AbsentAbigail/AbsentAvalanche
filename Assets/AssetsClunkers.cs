using System.Collections.Generic;
using AbsentAvalanche.Cards.Clunkers;

namespace AbsentAvalanche.Assets;

public static class AssetsClunkers
{
    public static void AddToAssets(List<object> assets)
    {
        assets.AddRange([
            new Boozle().Builder(),
            new PillowFortress().Builder()
        ]);
    }
}