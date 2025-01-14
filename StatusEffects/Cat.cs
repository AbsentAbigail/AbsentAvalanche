using AbsentAvalanche.StatusEffects.Implementations;
using AbsentUtilities;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
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

        status.targetConstraints =
        [
            TargetConstraintHelper.DoesTrigger(),
            TargetConstraintHelper.General<TargetConstraintDoesDamage>("Does Damage")
        ];
    })
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public override StatusEffectDataBuilder Builder()
    {
        return base.Builder()
            .WithTextInsert("{a}")
            .WithIcon_VFX("cat", "catkeyword", Keywords.Cat.NameWithGuid,
                VFXMod_StatusEffectHelpers.LayoutGroup.damage);
    }
}