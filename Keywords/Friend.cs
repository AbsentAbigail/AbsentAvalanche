using AbsentUtilities;

namespace AbsentAvalanche.Keywords;

internal class Friend() : AbstractKeyword(Name, "Friend",
    """
    When an ally would gain a negative status, apply it to me instead
    When an ally gains a positive status, gain half of it
    """)
{
    public const string Name = "friend";
}