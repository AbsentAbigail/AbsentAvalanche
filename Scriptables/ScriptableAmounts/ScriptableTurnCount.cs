#region

#endregion

namespace AbsentAvalanche.Scriptables.ScriptableAmounts;

internal class ScriptableTurnCount : ScriptableAmount
{
    public override int Get(Entity _)
    {
        return Battle.instance?.turnCount ?? 0;
    }
}