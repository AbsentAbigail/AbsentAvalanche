using System.Collections.Generic;
using AbsentAvalanche.Cards.Items;

namespace AbsentAvalanche.Assets;

public static class AssetsItems
{
    public static void AddToAssets(List<object> assets)
    {
        assets.AddRange([
            new NebulaAuxilium().Builder(),
            new Missile().Builder(),

            new NovaShard().Builder(),

            new BlingThrow().Builder(),

            new IceShard().Builder(),

            new Avarice().Builder(),

            new CursedClaymore().Builder(),

            new GhostlyPresence().Builder(),

            new Sarcophagus().Builder(),

            new RescueUFO().Builder(),

            new Catbom().Builder(),
            new CatomicBomb().Builder(),

            new Snowball().Builder(),
            new Blanket().Builder(),
            new CatToy().Builder(),
            new Pillow().Builder(),
            new ShadyBox().Builder(),
            new Headpat().Builder(),
            
            new HappyDreams().Builder(),
            new Imagination().Builder(),
            
            new NebulaInstrumenta().Builder(),
            
            new WoolGrenade().Builder(),
            new GoolWrenade().Builder(),
        ]);
    }
}