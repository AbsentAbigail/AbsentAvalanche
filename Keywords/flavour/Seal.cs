using AbsentAvalanche.Cards.Leaders;
using AbsentAvalanche.Patches;
using AbsentUtilities;
using Deadpan.Enums.Engine.Components.Modding;

namespace AbsentAvalanche.Keywords.flavour;

public class Seal() : AbstractKeyword(Name, "", Pronouns + "|" + Flavour)
{
    public const string Name = "seal" + "_flavour";
    private const string Pronouns = "He/Him";
    private static readonly string Flavour = new Cards.Companion.Seal().FlavourText;

    public override KeywordDataBuilder Builder()
    {
        return base.Builder()
            .WithBodyColour(Color(188, 188, 224))
            .SubscribeToAfterAllBuildEvent(_ =>
                CardPatches.Flavours =
                [
                    ..CardPatches.Flavours,
                    [AbsentUtils.PrefixGuid(Cards.Companion.Seal.Name), Name],
                    [AbsentUtils.PrefixGuid(Cards.Companion.Seal.Name + Leader<Cards.Companion.Bam>.Suffix), Name]
                ]
            );
    }
}