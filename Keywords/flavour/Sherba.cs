using AbsentAvalanche.Cards.Leaders;
using AbsentAvalanche.Patches;
using AbsentUtilities;
using Deadpan.Enums.Engine.Components.Modding;

namespace AbsentAvalanche.Keywords.flavour;

public class Sherba() : AbstractKeyword(Name, "", Pronouns + "|" + Flavour)
{
    public const string Name = "sherba" + "_flavour";
    private const string Pronouns = "She/Her";
    private static readonly string Flavour = new Cards.Companion.Sherba().FlavourText;

    public override KeywordDataBuilder Builder()
    {
        return base.Builder()
            .WithBodyColour(Color(188, 188, 224))
            .SubscribeToAfterAllBuildEvent(_ =>
                CardPatches.Flavours =
                [
                    ..CardPatches.Flavours,
                    [AbsentUtils.PrefixGuid(Cards.Companion.Sherba.Name), Name],
                    [AbsentUtils.PrefixGuid(Cards.Companion.Sherba.Name + Leader<Cards.Companion.Bam>.Suffix), Name]
                ]
            );
    }
}