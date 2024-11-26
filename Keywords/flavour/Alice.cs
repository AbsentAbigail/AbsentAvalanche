using AbsentAvalanche.Cards.Leaders;
using AbsentAvalanche.Patches;
using AbsentUtilities;
using Deadpan.Enums.Engine.Components.Modding;

namespace AbsentAvalanche.Keywords.flavour;

public class Alice() : AbstractKeyword(Name, "", Pronouns + "|" + Flavour)
{
    public const string Name = "alice" + "_flavour";
    private const string Pronouns = "She/Her";
    private static readonly string Flavour = new Cards.Companion.Alice().FlavourText;

    public override KeywordDataBuilder Builder()
    {
        return base.Builder()
            .WithBodyColour(Color(188, 188, 224))
            .SubscribeToAfterAllBuildEvent(_ =>
                CardPatches.Flavours =
                [
                    ..CardPatches.Flavours,
                    [AbsentUtils.PrefixGuid(Cards.Companion.Alice.Name), Name],
                    [AbsentUtils.PrefixGuid(Cards.Companion.Alice.Name + Leader<Cards.Companion.Alice>.Suffix), Name]
                ]
            );
    }
}