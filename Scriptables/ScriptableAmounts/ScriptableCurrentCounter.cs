using UnityEngine;

namespace AbsentAvalanche.Scriptables.ScriptableAmounts;

internal class ScriptableCurrentCounter : ScriptableAmount
{
    public float multiplier = 1f;
    public bool roundUp = false;

    public override int Get(Entity entity)
    {
        return !entity ? 0 : Mult(entity.counter.current);
    }

    private int Mult(int amount)
    {
        return !roundUp ? Mathf.FloorToInt(amount * multiplier) : Mathf.RoundToInt(amount * multiplier);
    }
}