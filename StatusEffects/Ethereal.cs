using AbsentAvalanche.StatusEffects.Implementations;
using AbsentUtilities;
using Deadpan.Enums.Engine.Components.Modding;
using WildfrostHopeMod.VFX;

namespace AbsentAvalanche.StatusEffects;

public class Ethereal() : AbstractStatus<StatusEffectEthereal>(
    Name,
    subscribe: data => { data.applyFormatKey = AbsentUtils.GetStatus("Shroom").applyFormatKey; })
{
    public const string Name = "Ethereal";

    public override StatusEffectDataBuilder Builder()
    {
        return base.Builder()
            .WithTextInsert("{a}")
            .WithIcon_VFX("ethereal", "ethereal", Keywords.Ethereal.NameWithGuid,
                VFXMod_StatusEffectHelpers.LayoutGroup.damage);
    }
}