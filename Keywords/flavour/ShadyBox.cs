using AbsentAvalanche.Patches;
using AbsentUtilities;
using Deadpan.Enums.Engine.Components.Modding;

namespace AbsentAvalanche.Keywords.flavour;

public class ShadyBox() : AbstractKeyword(Name, "", " |" + Flavour)
{
    public const string Name = "shadybox" + "_flavour";
    private static readonly string Flavour = new Cards.Items.ShadyBox().FlavourText;

    public override KeywordDataBuilder Builder()
    {
        return base.Builder()
            .WithBodyColour(Color(188, 188, 224))
            .SubscribeToAfterAllBuildEvent(_ =>
                CardPatches.Flavours =
                [
                    ..CardPatches.Flavours,
                    [AbsentUtils.PrefixGuid(Cards.Items.ShadyBox.Name), Name]
                ]
            );
    }
}