using System.Collections;
using AbsentAvalanche.Builders.Cards.Items;

namespace AbsentAvalanche.Scriptables.Scripts;

public class ScriptAddCardToDeck : Script
{
    public string cardToAdd = PlushBag.Name;

    public override IEnumerator Run()
    {
        var deck = References.PlayerData.inventory.deck;
        var card = Absent.GetCard(cardToAdd);
        deck.Add(card.Clone());
        CardDiscoverSystem.instance.DiscoverCard(card);
        yield break;
    }
}