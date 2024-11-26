using AbsentAvalanche.Patches;
using AbsentUtilities;
using Deadpan.Enums.Engine.Components.Modding;

namespace AbsentAvalanche.Keywords.flavour;

public class Eudora() : AbstractKeyword(Name, "", " |(Character by Gaziter\nSprites by Sunnaryi)")
{
    public const string Name = "eudora" + "_flavour";

    public override KeywordDataBuilder Builder()
    {
        return base.Builder()
            .WithBodyColour(Color(188, 188, 224))
            .SubscribeToAfterAllBuildEvent(_ =>
                CardPatches.Flavours =
                [
                    ..CardPatches.Flavours,
                    [AbsentUtils.PrefixGuid(Cards.Companion.Eudora.Name), Name]
                ]
            );
    }
}