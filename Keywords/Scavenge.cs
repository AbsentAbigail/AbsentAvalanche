using AbsentUtilities;
using Deadpan.Enums.Engine.Components.Modding;

namespace AbsentAvalanche.Keywords;

public class Scavenge()
    : AbstractKeyword(Name, "Scavenge", "On <boss> or <miniboss> kill, permanently gain a random charm")
{
    public const string Name = "scavenge";
    public static string Tag = GetTag(Name);

    public override KeywordDataBuilder Builder()
    {
        return base.Builder()
            .WithCanStack(true)
            .WithTitleColour(Color(72, 195, 157));
    }
}