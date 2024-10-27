using AbsentUtilities;
using Deadpan.Enums.Engine.Components.Modding;

namespace AbsentAvalanche.StatusEffects;

public class WhenEnemyIsHitByItemApplyWeaknessToThem() : AbstractApplyXStatus<StatusEffectApplyXWhenUnitIsHit>(Name)
{
    public const string Name = "When Enemey Is Hit By Item Apply Weakness To Them";

    public override StatusEffectDataBuilder Builder()
    {
        return AbsentUtils.StatusCopy("When Enemy Is Hit By Item Apply Demonize To Them", Name)
            .WithText("When an enemy is hit with an <Item>, apply <{a}><keyword=weakness> to them")
            .SubscribeToAfterAllBuildEvent(data =>
            {
                var status = (StatusEffectApplyXWhenUnitIsHit)data;

                status.effectToApply = AbsentUtils.GetStatus("Weakness");
            });
    }
}