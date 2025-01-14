using AbsentAvalanche.StatusEffects.Implementations;
using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.StatusEffects;

internal class GoldRushEffect() : AbstractStatus<StatusEffectGoldRush>(
    Name,
    subscribe: status =>
    {
        status.on = StatusEffectBonusDamageEqualToX.On.ScriptableAmount;
        status.scriptableAmount =
            ScriptableHelper.CreateScriptable<ScriptableGold>("0.02 of Gold", script => script.factor = 0.02f);
    })
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}