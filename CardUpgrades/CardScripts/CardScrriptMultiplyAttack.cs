using UnityEngine;

namespace AbsentAvalanche.CardUpgrades.CardScripts;

public class CardScriptMultiplyDamage : CardScript
{
    public float multiply;
    public bool roundUp;

    public override void Run(CardData target)
    {
        var damage = target.damage;
        damage = roundUp ? Mathf.CeilToInt(damage * multiply) : Mathf.RoundToInt(damage * multiply);
        target.damage = Mathf.Max(1, damage);
    }
}