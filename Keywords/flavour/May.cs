using AbsentAvalanche.Cards.Leaders;
using AbsentAvalanche.Patches;
using AbsentUtilities;
using Deadpan.Enums.Engine.Components.Modding;

namespace AbsentAvalanche.Keywords.flavour;

public class May() : AbstractKeyword(Name, "", Pronouns + "|" + Flavour + "\n(Sprites by Megamarine)")
{
    public const string Name = "may" + "_flavour";
    private const string Pronouns = "She/Her";
    private static readonly string Flavour = new Cards.Companion.May().FlavourText;

    public override KeywordDataBuilder Builder()
    {
        return base.Builder()
            .WithBodyColour(Color(188, 188, 224))
            .SubscribeToAfterAllBuildEvent(_ =>
                CardPatches.Flavours =
                [
                    ..CardPatches.Flavours,
                    [AbsentUtils.PrefixGuid(Cards.Companion.May.Name), Name],
                    [AbsentUtils.PrefixGuid(Cards.Companion.May.Name + Leader<Cards.Companion.Bam>.Suffix), Name]
                ]
            );
    }
}