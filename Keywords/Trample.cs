using AbsentUtilities;
using Deadpan.Enums.Engine.Components.Modding;

namespace AbsentAvalanche.Keywords;

public class Trample() : AbstractKeyword(Name, "Trample", "On kill, trigger an additional time")
{
    public const string Name = "trample";

    public override KeywordDataBuilder Builder()
    {
        return base.Builder()
            .WithTitleColour(Color(56, 235, 164));
    }
}