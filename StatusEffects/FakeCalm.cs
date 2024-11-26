using AbsentAvalanche.StatusEffects.Implementations;
using AbsentUtilities;
using Deadpan.Enums.Engine.Components.Modding;
using WildfrostHopeMod.VFX;

namespace AbsentAvalanche.StatusEffects;

internal class FakeCalm() : AbstractApplyXStatus<StatusEffectApplyXWhenDeployedOnce>(
    Name,
    effectToApply: Calm.Name
    )
{
    public const string Name = "FakeCalm";

    public override StatusEffectDataBuilder Builder()
    {
        return base.Builder()
            .WithTextInsert("{a}")
            .WithIcon_VFX("fakecalm", "calm", Keywords.Calm.NameWithGuid,
                VFXMod_StatusEffectHelpers.LayoutGroup.counter);
    }
}