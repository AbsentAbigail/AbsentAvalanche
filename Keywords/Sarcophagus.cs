using AbsentUtilities;

namespace AbsentAvalanche.Keywords;

public class Sarcophagus()
    : AbstractKeyword(Name, "Sarcophagus", $"4{Ethereal.Tag}\n" +
                                           $"Add <2> <of the sealed card> to hand when destroyed")
{
    public const string Name = "sarcophagus";
    public static string Tag = GetTag(Name);
}