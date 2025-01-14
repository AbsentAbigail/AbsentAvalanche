using AbsentAvalanche.Patches;
using AbsentUtilities;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;

namespace AbsentAvalanche.Keywords.flavour;

public class AliceAndNami() : AbstractKeyword(Name, "", Pronouns + "|" + Flavour + "\n(Sprites by Sunnaryi)")
{
    private const string Pronouns = "They/Them";
    private static readonly string Flavour = new Cards.Companion.AliceAndNami().FlavourText;
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name.ToLower() + "_flavour";

    public override KeywordDataBuilder Builder()
    {
        return base.Builder()
            .WithBodyColour(Color(188, 188, 224))
            .SubscribeToAfterAllBuildEvent(_ =>
                CardPatches.Flavours =
                [
                    ..CardPatches.Flavours,
                    [AbsentUtils.PrefixGuid(Cards.Companion.AliceAndNami.Name), Name]
                ]
            );
    }
}