#region

using System.Collections;
using System.Linq;

#endregion

namespace AbsentAvalanche.StatusEffectImplementations;

public class StatusEffectApplyXWhenCertainCardPlayed : StatusEffectApplyXOnCardPlayed
{
    public CardType allowedCardType;
    public CardData[] allowedCards = [];
    public TraitData[] allowedTraits = [];
    private Hit _hackyHit;

    public override void Init()
    {
        OnCardPlayed += Check;
    }

    public override bool RunCardPlayedEvent(Entity entity, Entity[] targets)
    {
        if (!target.enabled)
            return false;

        if (target == entity)
            return false;

        if (allowedCardType is not null && allowedCardType.name != entity.data.cardType.name)
            return false;

        var cardTraits = entity.traits.Select(t => t.data);

        var traitList = cardTraits.ToList();
        if (allowedTraits is { Length: > 0 } && !traitList.ToList().ContainsAny(allowedTraits))
            return false;

        _hackyHit = new Hit(entity, null);

        return allowedCards is not { Length: > 0 } || allowedCards.ToList().Any(c => c.name == entity.data.name);
    }

    public new IEnumerator Check(Entity entity, Entity[] targets)
    {
        return Run(GetTargets(_hackyHit, GetWasInRows(entity, targets), null, targets));
    }
}