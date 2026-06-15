using System.Collections.Generic;
using System.Linq;

namespace AbsentAvalanche.Scriptables.SelectScripts;

public class SelectScriptFurthestEnemies : SelectScript<Entity>
{
    public override List<Entity> Run(List<Entity> group)
    {
        var result = new List<Entity>();
        foreach (var entity in group)
        {
            var rowIndices = Battle.instance.GetRowIndices(entity);
            foreach (var rowIndex in rowIndices)
            {
                var row = Battle.instance.GetRow(entity.owner, rowIndex);
                var highestIndex = row.Select(entityInRow => row.IndexOf(entityInRow)).Prepend(0).Max();
                result.Add(row[highestIndex]);
            }
        }

        result.RemoveDuplicates();
        return result;
    }
}