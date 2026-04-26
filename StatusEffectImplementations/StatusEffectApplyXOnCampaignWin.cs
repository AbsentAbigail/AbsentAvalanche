#region

using AbsentAvalanche.Helpers;

#endregion

namespace AbsentAvalanche.StatusEffectImplementations;

public class StatusEffectApplyXOnCampaignWin : StatusEffectApplyX
{
    public override void Init()
    {
        Events.OnBattleEnd += Check;
    }

    public void OnDestroy()
    {
        Events.OnBattleEnd -= Check;
    }

    private void Check()
    {
        if (!Campaign.CheckVictory())
        {
            return;
        }
        
        LogHelper.Log("Campaign won, applying effect: " + effectToApply.name);
        ActionQueue.Stack(new ActionSequence(Run(GetTargets())));
    }
}