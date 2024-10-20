using System.Linq;

namespace AbsentAvalanche.StatusEffects.Scriptables;

internal class ScriptableTargetsOnBoard : ScriptableAmount
{
    public bool allies = false;
    public bool enemies = false;
    public bool inRow = false;

    public override int Get(Entity entity)
    {
        var result = 0;
        var rows = References.Battle.GetRowIndices(entity);

        if (inRow)
        {
            foreach (var row in rows)
            {
                if (allies) result += entity.GetAlliesInRow(row).Count;
                if (enemies) result += entity.GetEnemiesInRow(row).Count;
            }

            return result;
        }

        if (allies) result += entity.GetAllies().Count;
        if (enemies) result += entity.GetEnemies().Count;
        return result;
    }
}