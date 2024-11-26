using AbsentUtilities;
using Deadpan.Enums.Engine.Components.Modding;

namespace AbsentAvalanche.Keywords;

public class Royal()
    : AbstractKeyword(Name, "Royal", "Counts as an additional <Leader>|Lose when all leaders die")
{
    public const string Name = "royal";
    public static string Tag = GetTag(Name);

    public override KeywordDataBuilder Builder()
    {
        return base.Builder()
            .WithCanStack(true);
    }
}