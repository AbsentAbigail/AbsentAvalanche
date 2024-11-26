using System.Collections.Generic;
using AbsentAvalanche.Keywords.flavour;

namespace AbsentAvalanche.Assets;

public static class AssetsFlavours
{
    public static void AddToAssets(List<object> assets)
    {
        assets.AddRange([
            new Alice().Builder(),
            new Bam().Builder(),
            new Bamboozle().Builder(),
            new Jerry().Builder(),
            new LilGuy().Builder(),
            new May().Builder(),
            new Sam().Builder(),
            new Seal().Builder(),
            new Sherba().Builder(),
            new Catcus().Builder(),
            new Boozle().Builder(),
            new Elsta().Builder(),
            new PanickedNut().Builder(),
            new SalvoKitty().Builder(),
            new FusilladeCat().Builder(),
            new PillowFortress().Builder(),
            new Headpat().Builder(),
            new HappyDreams().Builder(),
            new Blanket().Builder(),
            new Imagination().Builder(),
            new Eudora().Builder(),
            new Lusine().Builder(),
            new ShadyBox().Builder(),
            new FrozenFlame().Builder(),
            new UnboundFlame().Builder(),
            new NebulaAuxilium().Builder()
        ]);
    }
}