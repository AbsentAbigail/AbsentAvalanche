namespace AbsentAvalanche.StatusEffectImplementations;

public class StatusEffectApplyXOnWin : StatusEffectApplyX
{
    public bool isBoss = true;
    public override void Init()
    {
        Events.OnBattleWin += Check;
    }

    public void OnDestroy()
    {
        Events.OnBattleWin -= Check;
    }

    private void Check()
    {
        if (!isBoss)
        {
            ActionQueue.Stack(new ActionSequence(Run(GetTargets())));
            return;
        }
        
        var playerNodeId = Campaign.FindCharacterNode(References.Player).id;
        if (Campaign.GetNode(playerNodeId).type.isBoss)
        {
            ActionQueue.Stack(new ActionSequence(Run(GetTargets())));
        }
    }
}