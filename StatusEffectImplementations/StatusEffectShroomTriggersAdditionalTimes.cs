using System.Collections;
using System.Collections.Generic;
using AbsentAvalanche.Helpers;

namespace AbsentAvalanche.StatusEffectImplementations;

internal class StatusEffectShroomTriggersAdditionalTimes : StatusEffectData
{
    private static List<Hit> _additionalHits = [];
    
    public override void Init()
    {
        OnHit += EchoShroom;
    }

    public override bool RunTurnStartEvent(Entity entity)
    {
        _additionalHits.Clear();
        return false;
    }

    public override bool RunHitEvent(Hit hit)
    {
        return !_additionalHits.Contains(hit) && hit.damageType == "shroom";
    }

    private IEnumerator EchoShroom(Hit hit)
    {
        for (var i = 0; i < GetAmount(); i++)
        {
            var echo = new Hit(hit.attacker, hit.target, hit.damage)
            {
                screenShake = 0.25f,
                damageType = "shroom"
            };
            _additionalHits.Add(echo);
            yield return echo.Process();
            yield return Sequences.Wait(0.3f);
        }
    }
}