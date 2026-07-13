using System.Linq;
using System.Threading.Tasks;
using AbsentAvalanche.Builders.Cards.Items;
using AbsentAvalanche.Builders.Traits;
using Random = UnityEngine.Random;

namespace AbsentAvalanche.EventHooks;

public static class MailpuppyHooks
{
    private static readonly string TraitName = Absent.GetTrait(Mailpuppy.Name).name;
    
    public static Task CampaignStart()
    {
        var deck = References.PlayerData.inventory.deck;
        var hasMailpuppy = deck.Any(card => card.traits.Any(trait => trait.data.name == TraitName));
        if (!hasMailpuppy)
        {
            return Task.CompletedTask;
        }

        AddInvitationToDeck();
        return Task.CompletedTask;
    }

    public static void EntityEnterBackpack(Entity entity)
    {
        if (entity.traits.All(trait => trait.data.name != TraitName))
        {
            return;
        }

        AddInvitationToDeck();
    }

    public static void AddInvitationToDeck()
    {
        var deck = References.PlayerData.inventory.deck;
        var invitation = GetInvitation().Clone();
        deck.Add(invitation);
        CardDiscoverSystem.instance.DiscoverCard(invitation);
        Campaign.PromptSave();
    }

    private static CardData GetInvitation()
    {
        var invitation = Absent.GetCard(Invitation.Name).Clone();
        
        if (Campaign.instance is null)
        {
            return invitation;
        }
        
        var playerNodeId = Campaign.FindCharacterNode(References.Player).id;
        foreach (var instanceNode in Campaign.instance.nodes.Where(instanceNode => instanceNode.type.isBattle))
        {
            if (instanceNode.id <= playerNodeId)
            {
                continue;
            }

            if (instanceNode.cleared)
            {
                continue;
            }
            
            var waves = (SaveCollection<BattleWaveManager.WaveData>)instanceNode.data["waves"];
            invitation.SetCustomData("absent.invitation", PickCardFromBattle(waves));

            break;
        }
        return invitation;
    }

    private static string PickCardFromBattle(SaveCollection<BattleWaveManager.WaveData> waves)
    {
        CardData card = null;
        for (var i = 0; i < 3; i++) // Retry twice for non-boss card
        {
            var wave = waves.collection.RandomItem();
            card = wave.PeekCardData(Random.Range(0, wave.Count));
            if (!card.cardType.miniboss)
            {
                return card.name;
            }
        }

        return card!.name; // Take boss
    }
}