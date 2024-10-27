using System.Collections;
using System.Linq;

namespace AbsentAvalanche.StatusEffects.Implementations;

internal class StatusEffectStress : StatusEffectBonusDamageEqualToX
{
    public override void Init()
    {
        PreCardPlayed += Gain;
    }

    private new IEnumerator Gain(Entity entity, Entity[] targets)
    {
        var num = FindOnBoard();
        switch (toReset)
        {
            case true when num == currentAmount:
                yield break;
            case true:
                LoseCurrentAmount();
                break;
        }

        if (num > 0) yield return GainAmount(num);
    }

    private new int FindOnBoard()
    {
        var damagedAllies = target.GetAllies().Count(e => e.hp.safeMax.Value > e.hp.safeCurrent.Value);
        return GetAmount() * damagedAllies;
    }
}