using AbsentAvalanche.StatusEffects.Implementations;
using AbsentUtilities;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using WildfrostHopeMod.VFX;

namespace AbsentAvalanche.StatusEffects;

internal class Abduct() : AbstractApplyXStatus<StatusEffectAbduct>(Name)
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    
    public override StatusEffectDataBuilder Builder()
    {
        return base.Builder()
            .WithTextInsert("{a}")
            .WithIcon_VFX(AbsentUtils.PrefixGuid("abduct"), "abduct", Keywords.Abduct.NameWithGuid,
                VFXMod_StatusEffectHelpers.LayoutGroup.counter);
    }
}