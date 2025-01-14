using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AbsentAvalanche.StatusEffects.Implementations;

public class StatusEffectApplyXToAllyInColumn : StatusEffectApplyXOnCardPlayed
{
    public override void Init()
    {
        OnCardPlayed += Check;
    }

    private new IEnumerator Check(Entity entity, Entity[] targets)
    {
        return Run(GetTargets());
    }

    private List<Entity> GetTargets()
    {
        var rows = Battle.instance.GetRows(target.owner);
        var index = target.containers.First().IndexOf(target);
        return rows.Select(row => row[index]).Where(e => e is not null && e != target).ToList();
    }
}