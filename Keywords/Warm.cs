using AbsentUtilities;

namespace AbsentAvalanche.Keywords;

public class Warm() : AbstractKeyword(Name, "Warm",
        "Every turn, count down <sprite name=snow>, <sprite name=frost> and <sprite name=block> of allies in row by <1>"
    )
{
    public const string Name = "warm";
}