using AbsentUtilities;
using Deadpan.Enums.Engine.Components.Modding;

namespace AbsentAvalanche.StatusEffects;

public class SummonSarcophagus() : AbstractStatus<StatusEffectSummon>(Name)
{
    public const string Name = "Summon Sarcophagus";

    public override StatusEffectDataBuilder Builder()
    {
        return AbsentUtils.StatusCopy("Summon Junk", Name);
    }
}