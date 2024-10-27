using AbsentUtilities;

namespace AbsentAvalanche.Cards.Items;

internal class NovaShard() : AbstractItem(
    Name, "Nova Shard",
    2, true,
    subscribe: card =>
    {
        card.attackEffects = [AbsentUtils.SStack("Block", 2)];
        card.startWithEffects = [AbsentUtils.SStack("Trigger Against Random Ally When Drawn")];
    })
{
    public const string Name = "NovaShard";
}