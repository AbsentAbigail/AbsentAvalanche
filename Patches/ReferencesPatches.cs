using HarmonyLib;
using JetBrains.Annotations;

namespace AbsentAvalanche.Patches;

[HarmonyPatch(typeof(References), nameof(References.Classes), MethodType.Getter)]
public static class ReferencesPatches
{
    [UsedImplicitly]
    static void Postfix(ref ClassData[] __result) => __result = AddressableLoader.GetGroup<ClassData>("ClassData").ToArray();
}