using System.Linq;

namespace AbsentAvalanche.GameSystems;

public class StopCompanionsSystem : GameSystem
{
    public void OnEnable()
    {
        Events.OnBattleStart += RemoveCompanions;
    }

    public void OnDisable()
    {
        Events.OnBattleStart -= RemoveCompanions;
    }

    private static void RemoveCompanions()
    {
        var companions = References.Player.drawContainer.Where(card => card.data.cardType.name == "Friendly").ToArray();
        foreach (var companion in companions)
        {
            companion.flipper.FlipUp();
            companion.RemoveFromContainers();
            companion.Kill();
        }
    }
}