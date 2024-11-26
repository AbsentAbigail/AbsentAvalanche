using AbsentUtilities;

namespace AbsentAvalanche.StatusEffects.Implementations;

public class StatusEffectApplyXOnBossKill : StatusEffectApplyXOnKill
{
    public CardType[] allowedTypes =
    [
        AbsentUtils.TryGet<CardType>("Boss"),
        AbsentUtils.TryGet<CardType>("BossSmall"),
        AbsentUtils.TryGet<CardType>("Miniboss")
    ];

    public override bool RunEntityDestroyedEvent(Entity entity, DeathType deathType)
    {
        if (!base.RunEntityDestroyedEvent(entity, deathType))
            return false;

        return allowedTypes.Contains(entity.data.cardType);
    }
}