using AbsentUtilities;
using Deadpan.Enums.Engine.Components.Modding;

namespace AbsentAvalanche.StatusEffects;

public class DummySummon() : AbstractStatus<StatusEffectData>(Name)
{
    public const string Name = "DummySummon";
    
    public override StatusEffectDataBuilder Builder()
    {
        return AbsentUtils.StatusCopy("Summon Junk", Name);
    }
}