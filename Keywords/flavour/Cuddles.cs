using AbsentAvalanche.Cards.Leaders;
using AbsentAvalanche.Patches;
using AbsentUtilities;
using Deadpan.Enums.Engine.Components.Modding;

namespace AbsentAvalanche.Keywords.flavour;

public class Cuddles() : AbstractKeyword(Name, "", Pronouns + "|" + Flavour)
{
    public const string Name = "cuddles" + "_flavour";
    private const string Pronouns = "He/Him";
    private static readonly string Flavour = new Cards.Companion.Cuddles().FlavourText;

    public override KeywordDataBuilder Builder()
    {
        return base.Builder()
            .WithBodyColour(Color(188, 188, 224))
            .SubscribeToAfterAllBuildEvent(_ =>
                CardPatches.Flavours =
                [
                    ..CardPatches.Flavours,
                    [AbsentUtils.PrefixGuid(Cards.Companion.Cuddles.Name), Name],
                    [AbsentUtils.PrefixGuid(Cards.Companion.Cuddles.Name + Leader<Cards.Companion.Alice>.Suffix), Name]
                ]
            );
    }
}