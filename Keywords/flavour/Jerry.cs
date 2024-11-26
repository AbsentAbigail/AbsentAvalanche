using AbsentAvalanche.Cards.Leaders;
using AbsentAvalanche.Patches;
using AbsentUtilities;
using Deadpan.Enums.Engine.Components.Modding;

namespace AbsentAvalanche.Keywords.flavour;

public class Jerry() : AbstractKeyword(Name, "", Pronouns + "|" + Flavour)
{
    public const string Name = "jerry" + "_flavour";
    private const string Pronouns = "They/Them";
    private static readonly string Flavour = new Cards.Companion.Jerry().FlavourText;

    public override KeywordDataBuilder Builder()
    {
        return base.Builder()
            .WithBodyColour(Color(188, 188, 224))
            .SubscribeToAfterAllBuildEvent(_ =>
                CardPatches.Flavours =
                [
                    ..CardPatches.Flavours,
                    [AbsentUtils.PrefixGuid(Cards.Companion.Jerry.Name), Name],
                    [AbsentUtils.PrefixGuid(Cards.Companion.Jerry.Name + Leader<Cards.Companion.Bam>.Suffix), Name]
                ]
            );
    }
}