using AbsentAvalanche.StatusEffects.Implementations;
using AbsentUtilities;

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
    public const string Name = "Gold Rush Effect";
}