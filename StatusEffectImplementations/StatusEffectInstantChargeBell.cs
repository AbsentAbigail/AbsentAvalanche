#region

using System.Collections;
using AbsentAvalanche.GameSystems;
using AbsentAvalanche.Helpers;

#endregion

namespace AbsentAvalanche.StatusEffectImplementations;

public class StatusEffectInstantChargeBell : StatusEffectInstant
{
    public bool fully = true;

    public override IEnumerator Process()
    {
        var redrawBellSystem = FindObjectOfType<RedrawBellSystem>(true);
        if (redrawBellSystem == null)
        {
            LogHelper.Log("No RedrawBellSystem found");
            yield return base.Process();
        }

        FindObjectOfType<ChargeRedrawBellSystem>(true)
            .ChargeRedrawBell(fully ? 0 : GetAmount());

        yield return Remove();
    }
}