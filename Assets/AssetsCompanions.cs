using System.Collections.Generic;
using AbsentAvalanche.Cards.Companion;

namespace AbsentAvalanche.Assets;

public static class AssetsCompanions
{
    public static void AddToAssets(List<object> assets)
    {
        assets.AddRange([
            new LilGuy().Builder(),
            new Jerry().Builder(),
            new Alice().Builder(),
            new Seal().Builder(),
            new Bam().Builder(),
            new Bamboozle().Builder(),
            new May().Builder(),
            new Sam().Builder(),
            new Sherba().Builder(),

            new Catcus().Builder(),
            new SalvoKitty().Builder(),
            new FusilladeCat().Builder(),

            new FrozenFlame().Builder(),
            new UnboundFlame().Builder(),

            new PanickedNut().Builder(),

            new Elsta().Builder(),

            new Lusine().Builder(),
            new Eudora().Builder(),

            new Blahaj().Builder(),
            new Aftonsparv().Builder(),
            new Blackfisk().Builder(),
            new Val().Builder(),
            new Kramig().Builder()
        ]);
    }
}