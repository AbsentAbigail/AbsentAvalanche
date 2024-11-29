using AbsentAvalanche.StatusEffects;
using AbsentUtilities;
using Deadpan.Enums.Engine.Components.Modding;

namespace AbsentAvalanche.Cards.Items;

internal class NebulaInstrumenta() : AbstractItem(
    Name, "Nebula Instrumenta",
    pools: Pools.None,
    subscribe: card =>
    {
        card.traits = [
            AbsentUtils.TStack("Zoomlin"),
            AbsentUtils.TStack("Consume"),
        ];
        card.startWithEffects =
        [
            AbsentUtils.SStack(OnCardPlayedTutorRandomTreasure.Name)
        ];
    })
{
    public const string Name = "NebulaInstrumenta";
    public override string FlavourText => "Space donut";

    public override CardDataBuilder Builder()
    {
        return base.Builder()
            .SetAddressableSprites(NebulaAuxilium.Name, AltSprite);
    }
}