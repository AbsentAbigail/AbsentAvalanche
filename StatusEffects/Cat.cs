using AbsentAvalanche.StatusEffects.Implementations;
using AbsentUtilities;
using Deadpan.Enums.Engine.Components.Modding;
using WildfrostHopeMod.VFX;

namespace AbsentAvalanche.StatusEffects;

public class Cat() : AbstractStatus<StatusEffectCat>(
    Name,
    subscribe: status =>
    {
        status.applyFormatKey = AbsentUtils.GetStatus("Shroom").applyFormatKey;
        status.doesDamage = true;
        status.dealDamage = true;
        status.countsAsHit = true;
        status.applyEqualAmount = true;
    })
{
    public const string Name = "Cat";

    public override StatusEffectDataBuilder Builder()
    {
        return base.Builder()
            .WithTextInsert("{a}")
            .WithOffensive(true)
            .WithIcon_VFX("cat", "catkeyword", Keywords.Cat.NameWithGuid,
                VFXMod_StatusEffectHelpers.LayoutGroup.damage);
    }
}