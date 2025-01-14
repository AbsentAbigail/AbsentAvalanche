using AbsentAvalanche.StatusEffects.Implementations;
using AbsentUtilities;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using WildfrostHopeMod.VFX;

namespace AbsentAvalanche.StatusEffects;

public class Ethereal() : AbstractStatus<StatusEffectEthereal>(
    Name,
    subscribe: data => { data.applyFormatKey = AbsentUtils.GetStatus("Shroom").applyFormatKey; })
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public override StatusEffectDataBuilder Builder()
    {
        return base.Builder()
            .WithTextInsert("{a}")
            .WithIcon_VFX("ethereal", "ethereal", Keywords.Ethereal.NameWithGuid,
                VFXMod_StatusEffectHelpers.LayoutGroup.damage);
    }
}