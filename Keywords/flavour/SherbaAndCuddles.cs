using AbsentAvalanche.Cards.Leaders;
using AbsentAvalanche.Patches;
using AbsentUtilities;
using Deadpan.Enums.Engine.Components.Modding;

namespace AbsentAvalanche.Keywords.flavour;

public class SherbaAndCuddles() : AbstractKeyword(Name, "", Pronouns + "|" + Flavour)
{
    public const string Name = "sherbaandcuddles" + "_flavour";
    private const string Pronouns = "They/Them";
    private static readonly string Flavour = new Cards.Companion.SherbaAndCuddles().FlavourText;

    public override KeywordDataBuilder Builder()
    {
        return base.Builder()
            .WithBodyColour(Color(188, 188, 224))
            .SubscribeToAfterAllBuildEvent(_ =>
                CardPatches.Flavours =
                [
                    ..CardPatches.Flavours,
                    [AbsentUtils.PrefixGuid(Cards.Companion.SherbaAndCuddles.Name), Name],
                    [AbsentUtils.PrefixGuid(Cards.Companion.SherbaAndCuddles.Name + Leader<Cards.Companion.Alice>.Suffix), Name]
                ]
            );
    }
}