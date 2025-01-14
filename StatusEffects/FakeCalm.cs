using AbsentAvalanche.StatusEffects.Implementations;
using AbsentUtilities;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using WildfrostHopeMod.VFX;

namespace AbsentAvalanche.StatusEffects;

internal class FakeCalm() : AbstractApplyXStatus<StatusEffectApplyXWhenDeployedOnce>(
    Name,
    effectToApply: Calm.Name
    )
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public override StatusEffectDataBuilder Builder()
    {
        return base.Builder()
            .WithTextInsert("{a}")
            .WithIcon_VFX("fakecalm", "calm", Keywords.Calm.NameWithGuid,
                VFXMod_StatusEffectHelpers.LayoutGroup.counter);
    }
}