namespace AbsentAvalanche.Scriptables.CardScripts;

public class CardScriptSwapHealthAttack : CardScript
{
    public override void Run(CardData target)
    {
        (target.damage, target.hp) = (target.hp, target.damage);
    }
}