namespace AbsentAvalanche.StatusEffectImplementations;

public class StatusEffectApplyXOnBossKill : StatusEffectApplyXOnKill
{
    public CardType[] allowedTypes =
    [
        Absent.GetCardType("Boss"),
        Absent.GetCardType("BossSmall"),
        Absent.GetCardType("Miniboss")
    ];

    public override bool RunEntityDestroyedEvent(Entity entity, DeathType deathType)
    {
        return base.RunEntityDestroyedEvent(entity, deathType) && allowedTypes.Contains(entity.data.cardType);
    }
}