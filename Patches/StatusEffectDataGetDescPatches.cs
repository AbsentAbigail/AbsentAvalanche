using System.Linq;
using AbsentAvalanche.Builders.StatusEffects;
using AbsentAvalanche.Helpers;
using HarmonyLib;
using JetBrains.Annotations;

namespace AbsentAvalanche.Patches;

[HarmonyPatch(typeof(StatusEffectData), nameof(StatusEffectData.GetDesc))]
public class StatusEffectDataGetDescPatches
{
    private static readonly string BossExplorerEffectName = Absent.PrefixGuid(ExplorerDefeatBossFight.Name);
    private static readonly string InvitationEffectName = Absent.PrefixGuid(Invitation.Name);
    
    [UsedImplicitly]
    public static void Prefix(StatusEffectData __instance)
    {
        ExplorerLilGuy(__instance);
        MailpuppySam(__instance);
    }

    private static void ExplorerLilGuy(StatusEffectData instance)
    {
        if (instance.name != BossExplorerEffectName)
        {
            return;
        }
        instance.textInsert = "Unknown";
        
        if (Campaign.instance is null)
        {
            return;
        }
        
        var playerNodeId = Campaign.FindCharacterNode(References.Player).id;
        foreach (var instanceNode in Campaign.instance.nodes.Where(instanceNode => instanceNode.type.isBoss))
        {
            if (instanceNode.id < playerNodeId)
            {
                continue;
            }

            if (instanceNode.cleared)
            {
                continue;
            }
            
            var battleName = (string)instanceNode.data["battle"];
            
            var battleData = AddressableLoader.Get<BattleData>(nameof(BattleData), battleName);
            if (battleData?.nameRef is not { IsEmpty: false })
            {
                return;
            }
            var textInsert = $"<{battleData.nameRef?.GetLocalizedString()}>";

            instance.textInsert = textInsert;
            return;
        }
    }

    private static void MailpuppySam(StatusEffectData instance)
    {
        if (instance.name != InvitationEffectName)
        {
            return;
        }
        instance.textInsert = "no one...";

        var invitationTarget = instance.target.data.GetCustomDataOrNull("absent.invitation") as string;
        if (invitationTarget.IsNullOrEmpty())
        {
            return;
        }

        instance.textInsert = $"<card={invitationTarget}>";
    }
}