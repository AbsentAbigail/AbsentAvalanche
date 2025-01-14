using System.Collections.Generic;
using AbsentAvalanche.CardUpgrades;

namespace AbsentAvalanche.Assets;

public static class AssetsCardUpgrades
{
    public static void AddToAssets(List<object> assets)
    {
        assets.AddRange([
            new CardUpgradeShark().Builder(),
            new CardUpgradeMitosis().Builder(),
            new CardUpgradeViolence().Builder(),
            new CardUpgradeCursed().Builder(),
            new CardUpgradeCat().Builder(),
            new CardUpgradeBravery().Builder(),
            new CardUpgradeWill().Builder(),
            new CardUpgradeFortitude().Builder(),
            new CardUpgradeValor().Builder(),
            new CardUpgradeSarcophagus().Builder(),
            new CardUpgradeChangeLeader().Builder(),
        ]);
    }
}