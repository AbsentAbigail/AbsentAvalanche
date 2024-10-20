using AbsentUtilities;
using Deadpan.Enums.Engine.Components.Modding;

namespace AbsentAvalanche.StatusEffects;

internal class InstantSummonMissileInHand() : AbstractStatus<StatusEffectData>(Name)
{
    public const string Name = "Instant Summon Missile In Hand";

    public override StatusEffectDataBuilder Builder()
    {
        return AbsentUtils.StatusCopy("Instant Summon Junk In Hand", Name)
            .SubscribeToAfterAllBuildEvent(data =>
                ((StatusEffectInstantSummon)data).targetSummon = AbsentUtils.GetStatusOf<StatusEffectSummon>(SummonMissile.Name));
    }
}