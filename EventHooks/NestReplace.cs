#region

using System.Linq;
using Dead;

#endregion

namespace AbsentAvalanche.EventHooks;

public static class NestReplace
{
    private static readonly string NestName = Absent.GetCard("Nest").name;
    
    public static void Replace()
    {
        var deck = References.PlayerData.inventory.deck;
        var nest = deck.FirstOrDefault(card => card.name == NestName);
        if (nest == null)
        {
            return;
        }

        deck.Remove(nest);
        var random = Random.Range(1, 5001);
        var card = random switch
        {
            <= 2500 => Absent.GetCard("LeafEgg"),
            <= 5000 => Absent.GetCard("CuddleEgg"),
            _ => Absent.GetCard("Egg")
        };
        deck.Add(card.Clone());
        CardDiscoverSystem.instance.DiscoverCard(card);
    }
}