using AbsentAvalanche.Cards.Leaders;
using AbsentAvalanche.Patches;
using AbsentUtilities;
using Deadpan.Enums.Engine.Components.Modding;

namespace AbsentAvalanche.Keywords.flavour;

public class Bubbles() : AbstractKeyword(Name, "", Pronouns + "|" + Flavour)
{
    public const string Name = "bubbles" + "_flavour";
    private const string Pronouns = "She/Her";
    private static readonly string Flavour = new Cards.Companion.Bubbles().FlavourText;

    public override KeywordDataBuilder Builder()
    {
        return base.Builder()
            .WithBodyColour(Color(188, 188, 224))
            .SubscribeToAfterAllBuildEvent(_ =>
                CardPatches.Flavours =
                [
                    ..CardPatches.Flavours,
                    [AbsentUtils.PrefixGuid(Cards.Companion.Bubbles.Name), Name],
                    [AbsentUtils.PrefixGuid(Cards.Companion.Bubbles.Name + Leader<Cards.Companion.Alice>.Suffix), Name],
                    ["verdego.wildfrost.specialdelivery.AbigailIsakai", Name],
                ]
            );
    }
}