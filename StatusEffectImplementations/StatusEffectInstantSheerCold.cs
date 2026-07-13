using System.Collections;
using AbsentAvalanche.GameSystems;
using Deadpan.Enums.Engine.Components.Modding;
using UnityEngine;
using UnityEngine.Localization;

namespace AbsentAvalanche.StatusEffectImplementations;

internal class StatusEffectInstantSheerCold : StatusEffectInstantKillOrPhase
{
    public int odds = 3;

    public LocalizedString hitMessage =
        LocalizationHelper.GetCollection("UI Text", SystemLanguage.English).GetString("SheerCold");
    public LocalizedString missMessage = 
        LocalizationHelper.GetCollection("UI Text", SystemLanguage.English).GetString("SheerColdMiss");

    public override IEnumerator Process()
    {
        var hit = Random.Range(0, odds) == 0;
        
        var text = hit ? hitMessage : missMessage;
        yield return CustomTextPopupSystem.RunNoWait(target, text);

        if (hit)
        {
            yield return base.Process();
        }
        else
        {
            yield return Remove();
        }
    }
}