using AbsentUtilities;

namespace AbsentAvalanche.Keywords;

public class Valor()
    : AbstractKeyword(Name, "Valour", "Hits target in row with highest <keyword=attack>|Frontmost unit breaks ties")
{
    public const string Name = "valor";
    public static string Tag = GetTag(Name);
}