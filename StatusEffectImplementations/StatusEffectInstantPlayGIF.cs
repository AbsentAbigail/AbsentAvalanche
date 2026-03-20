#region

using System.Collections;
using UnityEngine;

#endregion

namespace AbsentAvalanche.StatusEffectImplementations;

public class StatusEffectInstantPlayGif : StatusEffectInstant
{
    public string animationKey;
    public string soundKey;
    public Vector3 positionOffset;
    public Vector3 scale = new(1, 1, 1);
    public float waitFor;

    public override IEnumerator Process()
    {
        VFXHelper.SFX.TryPlaySound(soundKey);
        VFXHelper.VFX.TryPlayEffect(
            animationKey,
            target.transform.position.Add(positionOffset),
            UtilityScript.Multiply(target.transform.lossyScale, scale));
        yield return new WaitForSeconds(waitFor);
        yield return base.Process();
    }
}