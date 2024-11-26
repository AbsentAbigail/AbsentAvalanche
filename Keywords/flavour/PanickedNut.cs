using AbsentAvalanche.Patches;
using AbsentUtilities;
using Deadpan.Enums.Engine.Components.Modding;

namespace AbsentAvalanche.Keywords.flavour;

public class PanickedNut() : AbstractKeyword(Name, "", Pronouns + "|" + Flavour)
{
    public const string Name = "panickednut" + "_flavour";
    private const string Pronouns = "It/Its";
    private static readonly string Flavour = new Cards.Companion.PanickedNut().FlavourText;

    public override KeywordDataBuilder Builder()
    {
        return base.Builder()
            .WithBodyColour(Color(188, 188, 224))
            .SubscribeToAfterAllBuildEvent(_ =>
                CardPatches.Flavours =
                [
                    ..CardPatches.Flavours,
                    [AbsentUtils.PrefixGuid(Cards.Companion.PanickedNut.Name), Name]
                ]
            );
    }
}