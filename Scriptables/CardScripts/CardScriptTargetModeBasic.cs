#region

using Deadpan.Enums.Engine.Components.Modding;

#endregion

namespace AbsentAvalanche.Scriptables.CardScripts;

public class CardScriptTargetModeBasic : CardScript
{
    public override void Run(CardData target)
    {
        target.targetMode = Extensions.GetTargetMode("TargetModeBasic");
        target.playType = Card.PlayType.Play;
        target.needsTarget = true;
        target.playOnSlot = false;
        target.canPlayOnHand = false;
        target.canPlayOnBoard = true;
        target.canPlayOnEnemy = true;
        target.canPlayOnFriendly = true;
    }
}