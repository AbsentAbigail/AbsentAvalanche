using AbsentUtilities;

namespace AbsentAvalanche.Keywords;

public class Panic() : AbstractKeyword(Name, "Panic", "When deployed, gain for each enemy")
{
    public const string Name = "panic";
    public static string Tag = GetTag(Name);
}