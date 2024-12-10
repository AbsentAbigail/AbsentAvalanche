using System.Collections.Generic;
using AbsentAvalanche.CardUpgrades;

namespace AbsentAvalanche.Assets;

public static class AssetsCardUpgrades
{
    public static void AddToAssets(List<object> assets)
    {
        assets.AddRange([
            new SharkCharm().Builder(),
            new MitosisCharm().Builder(),
            new ViolenceCharm().Builder(),
            new CursedCharm().Builder(),
            new CatCharm().Builder(),
            new BraveryButton().Builder(),
            new WillButton().Builder(),
            new FortitudeButton().Builder(),
            new ValorButton().Builder(),
            new SarcophagusCharm().Builder(),
            new CardUpgradeChangeLeader().Builder(),
        ]);
    }
}