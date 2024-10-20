using AbsentUtilities;

namespace AbsentAvalanche.Keywords;

internal class GoldRush() : AbstractKeyword(Name, "Gold Rush",
    "On kill, gain <1> additional damage for each <50><keyword=blings> for the next attack")
{
    public const string Name = "goldrush";
}