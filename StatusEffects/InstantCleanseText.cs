using AbsentUtilities;
using Deadpan.Enums.Engine.Components.Modding;

namespace AbsentAvalanche.StatusEffects;

public class InstantCleanseText() : AbstractStatus<StatusEffectData>(Name)
{
    public const string Name = "InstantCleanseText";

    public override StatusEffectDataBuilder Builder()
    {
        return AbsentUtils.StatusCopy("Cleanse", Name)
            .WithText("<keyword=cleanse>");
    }
}