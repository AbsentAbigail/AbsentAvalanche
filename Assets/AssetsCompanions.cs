using System.Collections.Generic;
using AbsentAvalanche.Cards.Companion;
using AbsentAvalanche.Cards.Leaders;

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
            new Chirp().Builder(),
            new Cuddles().Builder(),
            new Bubbles().Builder(),
            
            new BubblesAndCuddles().Builder(),
            new Leader<BubblesAndCuddles>().Builder(),
            new SherbaAndCuddles().Builder(),
            new Leader<SherbaAndCuddles>().Builder(),

            new Catcus().Builder(),
            new Catcitten().Builder(),
            new Catci().Builder(),
            
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