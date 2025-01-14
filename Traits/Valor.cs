using AbsentAvalanche.StatusEffects;
using AbsentUtilities;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;

namespace AbsentAvalanche.Traits;

public class Valor() : AbstractTrait(Name, Keywords.Valor.Name, HitHighestAttack.Name)
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

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