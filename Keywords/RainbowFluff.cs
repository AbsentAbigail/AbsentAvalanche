using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.Keywords;

public class RainbowFluff() : AbstractKeyword(Name, "Rainbow Fluff", 
    """
    Randomly applies one of:
    
    <sprite name=catkeyword>, <sprite name=block>, <sprite name=frenzy>, <sprite name=shell>, <sprite name=spice>, <sprite name=teeth>, Count down <sprite name=counter>, Reduce <sprite name=counter>
    """)
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name.ToLower();
    public static string Tag = GetTag(Name);
}