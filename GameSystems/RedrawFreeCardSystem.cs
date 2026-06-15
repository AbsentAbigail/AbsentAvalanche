using System.Linq;
using AbsentAvalanche.Builders.StatusEffects;

namespace AbsentAvalanche.GameSystems;

public class RedrawFreeCardSystem : GameSystem
{
    private readonly StatusEffectData _statusToApply = Absent.GetStatus(FreePlay.Name);
    
    public void OnEnable()
    {
        Events.OnRedrawBellHit += ApplyFreePlay;
    }

    public void OnDisable()
    {
        Events.OnRedrawBellHit -= ApplyFreePlay;
    }

    private void ApplyFreePlay(RedrawBellSystem redrawBellSystem)
    {
        var entity = References.Player.entity.GetAllAllies().FirstOrDefault(ally => ally.data.cardType.miniboss);
        if (entity)
        {
            ActionQueue.Stack(new ActionApplyStatus(entity, null, _statusToApply, 1));
        }
    }
}