using AbsentUtilities;
using Deadpan.Enums.Engine.Components.Modding;

namespace AbsentAvalanche.Keywords;

public class Rest()
    : AbstractKeyword(Name, "Rest", $"Increase {Ethereal.Tag} to match <Rest> when played or discarded")
{
    public const string Name = "rest";

    public override KeywordDataBuilder Builder()
    {
        return base.Builder()
            .WithCanStack(true)
            .WithTitleColour(Color(253, 97, 195));
    }
}