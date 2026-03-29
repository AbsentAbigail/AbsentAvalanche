#region

using System.Linq;
using AbsentAvalanche.Builders.StatusEffects;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Patches;

[HarmonyPatch(typeof(StatusEffectData), nameof(StatusEffectData.GetDesc))]
public class StatusEffectDataGetDescPatches
{
    private static readonly string BossExplorerEffectName = Absent.PrefixGuid(ExplorerDefeatBossFight.Name);
    
    [UsedImplicitly]
    public static void Prefix(StatusEffectData __instance)
    {
        if (__instance.name != BossExplorerEffectName)
        {
            return;
        }
        __instance.textInsert = "Unknown";
        
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

            __instance.textInsert = textInsert;
            return;
        }
    }
}