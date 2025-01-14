using AbsentUtilities;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;

namespace AbsentAvalanche.Keywords;

public class Trample() : AbstractKeyword(Name, "Trample", "On kill, trigger an additional time")
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name.ToLower();

    public override KeywordDataBuilder Builder()
    {
        return base.Builder()
            .WithTitleColour(Color(56, 235, 164));
    }
}