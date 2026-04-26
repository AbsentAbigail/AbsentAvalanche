namespace AbsentAvalanche.GameSystems;

public class InitialSnowEnemiesSystem : GameSystem
{
    private readonly StatusEffectData _statusToApply = Absent.GetStatus("Snow");
    
    public void OnEnable()
    {
        Events.OnEntityEnabled += Snow;
    }
    
    public void OnDisable()
    {
        Events.OnEntityEnabled -= Snow;
    }

    private void Snow(Entity entity)
    {
        if (entity.owner == References.Player)
        {
            return;
        }

        if (BattleSaveSystem.instance.loading)
        {
            return;
        }
        
        ActionQueue.Stack(new ActionApplyStatus(entity, null, _statusToApply, 1));
    }
}