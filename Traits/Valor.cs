using AbsentAvalanche.StatusEffects;
using AbsentUtilities;
using Deadpan.Enums.Engine.Components.Modding;

namespace AbsentAvalanche.Traits;

public class Valor() : AbstractTrait(Name, Keywords.Valor.Name, HitHighestAttack.Name)
{
    public const string Name = "Valor";

    public override TraitDataBuilder Builder()
    {
        return base.Builder()
            .WithOverrides(
                AbsentUtils.GetTrait("Barrage"),
                AbsentUtils.GetTrait("Aimless"),
                AbsentUtils.GetTrait("Longshot")
            );
    }
}