// #name Allow overlapping in build mode
// #author ghorsington
// #desc Allows to overlap props in build mode.

using BepInEx.Harmony;
using HarmonyLib;
using Housing;

public static class IgnoreOverlap {
    static Harmony instance;

    public static void Main() {
        instance = HarmonyWrapper.PatchAll(typeof(IgnoreOverlap));
    }

    public static void Unload() {
        instance.UnpatchAll(instance.Id);
        instance = null;
    }
    
    [HarmonyPatch(typeof(CraftInfo), "IsOverlapNow", MethodType.Getter)]
    [HarmonyPatch(typeof(OCItem), "IsOverlapNow", MethodType.Getter)]
    [HarmonyPatch(typeof(OCFolder), "IsOverlapNow", MethodType.Getter)]
    [HarmonyPatch(typeof(ObjectCtrl), "IsOverlapNow", MethodType.Getter)]
    [HarmonyPrefix]
    public static bool GetIsOverlapNow(ref bool __result) {
        __result = false;
        return false;
    }
}