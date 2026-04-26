#region

using System.Collections;
using System.Linq;

#endregion

namespace AbsentAvalanche.StatusEffectImplementations;

internal class StatusEffectInstantKillOrPhase : StatusEffectInstant
{
    public string[] statusesToClear =
    [
        "block",
        "shell",
        "scrap"
    ];

    public override IEnumerator Process()
    {
        var clump = new Routine.Clump();
        foreach (var typeToClear in statusesToClear)
        {
            var status = target.FindStatus(typeToClear);
            if (status != null)
                clump.Add(status.Remove());
        }
        yield return clump.WaitForEnd();
        foreach (var statusEffectData in target.statusEffects.Where(s => s is StatusEffectNextPhase))
        {
            var nextPhase = (StatusEffectNextPhase)statusEffectData;
            nextPhase.TryActivate();
            yield break;
        }
        target.forceKill = DeathType.Normal;
        yield return base.Process();
    }
}