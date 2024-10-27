using AbsentAvalanche.StatusEffects.Implementations;
using AbsentUtilities;
using Deadpan.Enums.Engine.Components.Modding;
using WildfrostHopeMod.VFX;

namespace AbsentAvalanche.StatusEffects;

internal class Abduct() : AbstractApplyXStatus<StatusEffectAbduct>(Name)
{
    public const string Name = "Abduct";
    
    public override StatusEffectDataBuilder Builder()
    {
        return base.Builder()
            .WithTextInsert("{a}")
            .WithIcon_VFX("abduct", "abduct", Keywords.Abduct.NameWithGuid,
                VFXMod_StatusEffectHelpers.LayoutGroup.counter);
    }
}