using System;
using System.Collections;
using System.Linq;
using AbsentUtilities;

namespace AbsentAvalanche.StatusEffects.Implementations;

public class StatusEffectInstantChangeStatsPermanent : StatusEffectInstant
{
    public bool health = true;
    public bool damage;
    public bool counter;
    public bool increase = true;
    private CardData _deckCopy;
    private int _change;
    
    public override IEnumerator Process()
    {
        _deckCopy = References.PlayerData.inventory.deck.FirstOrDefault(c => target.data.id == c.id);
        if (_deckCopy is null)
        {
            LogHelper.Error("No deck copy found");
            yield return Remove();
            yield break;   
        }
        _change = GetAmount() * (increase ? 1 : -1);
        
        if (health)
            ChangeHealth();
        if (damage)
            ChangeDamage();
        if (counter)
            ChangeCounter();

        Campaign.PromptSave();
        yield return Remove();
    }

    public void ChangeHealth()
    {
        _deckCopy.hp += _change;
        target.hp.max += _change;
        target.hp.current += _change;
    }
    
    public void ChangeDamage()
    {
        _deckCopy.damage += _change;
        target.damage.max += _change;
        target.damage.current += _change;
    }
    
    public void ChangeCounter()
    {
        if (target.counter > 0)
        {
            target.counter.max = Math.Max(1, target.counter.max + _change);
            target.counter.current = target.counter.current;
        }
        if (_deckCopy.counter > 0)
            _deckCopy.counter = Math.Max(1, _deckCopy.counter + _change);
    }
}