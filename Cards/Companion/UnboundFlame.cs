using AbsentAvalanche.Helpers;
using AbsentAvalanche.StatusEffects;

namespace AbsentAvalanche.Cards.Companion
{
    internal class UnboundFlame() : AbstractUnit(
        Name, "Unbound Flame",
        5, 0, 3,
        Pools.None,
        card =>
        {
            card.attackEffects = [Absent.SStack("Overload", 3)];
            card.startWithEffects = [
                Absent.SStack(OnCardPlayedApplyOverloadToAlliesInRow.Name, 3),
            ];
            card.traits = [Absent.TStack("Barrage")];
        })
    {
        public const string Name = "UnboundFlame";
    }
}