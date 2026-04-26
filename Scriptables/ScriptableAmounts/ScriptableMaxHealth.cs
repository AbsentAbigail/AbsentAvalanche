#region

#endregion

namespace AbsentAvalanche.Scriptables.ScriptableAmounts;

internal class ScriptableMaxHealth : ScriptableAmount
{
    public override int Get(Entity entity)
    {
        return !entity || !entity.data.hasHealth ? 0 : entity.hp.max;
    }
}