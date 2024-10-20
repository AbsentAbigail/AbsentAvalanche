using AbsentAvalanche.StatusEffects;
using AbsentUtilities;
using Deadpan.Enums.Engine.Components.Modding;

namespace AbsentAvalanche.Cards.Companion;

internal class PanickedNut() : AbstractUnit(
    Name, "Panicked Nut",
    3, 1, 3,
    Pools.Snowdweller,
    card =>
    {
        card.startWithEffects =
        [
            AbsentUtils.SStack(WhenDeployedGainShellForEachEnemy.Name, 3),
            AbsentUtils.SStack(WhenDeployedGainSnowForEachEnemy.Name, 1),
            AbsentUtils.SStack("Teeth", 1)
        ];
    })
{
    public const string Name = "PanickedNut";

    public override CardDataBuilder Builder()
    {
        return base.Builder()
            .WithText("When deployed, gain <{s0}><keyword=shell> and <{s1}><keyword=snow> for each enemy");
    }
}