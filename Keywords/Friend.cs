using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.Keywords;

internal class Friend() : AbstractKeyword(Name, "Friend",
    """
    When an ally would gain a negative status, apply it to me instead
    When an ally gains a positive status, gain half of it
    """)
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name.ToLower();
}