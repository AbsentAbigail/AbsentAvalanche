#region

using System.Collections;

#endregion

namespace AbsentAvalanche.StatusEffectImplementations;

public class StatusEffectAttackWithTeeth : StatusEffectData
{
    public string attackWithType = Absent.GetStatus("Teeth").type;

    public override void Init()
    {
        PreAttack += Check;
    }

    private IEnumerator Check(Hit hit)
    {
        if (hit.attacker != target)
            yield break;

        var attack = target.FindStatus(attackWithType)?.count ?? 0;

        hit.damage = attack;
    }
}