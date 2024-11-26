using AbsentAvalanche.Patches;
using AbsentUtilities;
using Deadpan.Enums.Engine.Components.Modding;

namespace AbsentAvalanche.Keywords.flavour;

public class HappyDreams() : AbstractKeyword(Name, "", " |" + Flavour)
{
    public const string Name = "happydreams" + "_flavour";
    private static readonly string Flavour = new Cards.Items.HappyDreams().FlavourText;

    public override KeywordDataBuilder Builder()
    {
        return base.Builder()
            .WithBodyColour(Color(188, 188, 224))
            .SubscribeToAfterAllBuildEvent(_ =>
                CardPatches.Flavours =
                [
                    ..CardPatches.Flavours,
                    [AbsentUtils.PrefixGuid(Cards.Items.HappyDreams.Name), Name]
                ]
            );
    }
}