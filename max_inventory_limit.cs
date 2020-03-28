// #name Max inventory
// #author ghorsington
// #desc Set inventory size to 99999. Essentially unlimited space.

using BepInEx.Harmony;
using HarmonyLib;
using AIProject.UI;
using AIProject;
using AIProject.SaveData;
using AIProject.UI.Viewer;

public static class MaxInventoryLimit {
    static Harmony instance;

    public static void Main() {
        instance = HarmonyWrapper.PatchAll(typeof(MaxInventoryLimit));
    }

    public static void Unload() {
        instance.UnpatchAll(instance.Id);
        instance = null;
    }

    [HarmonyPatch(typeof(DefinePack.MapGroup), "ItemStackUpperLimit", MethodType.Getter)]
    [HarmonyPrefix]
    public static bool GetItemStackUpperLimit(ref int __result) {
        __result = 99999;
        return false;
    }

    [HarmonyPatch(typeof(DefinePack.MapGroup), "ItemSlotMax", MethodType.Getter)]
    [HarmonyPrefix]
    public static bool GetItemSlotMax(ref int __result) {
        __result = 99999;
        return false;
    }

    [HarmonyPatch(typeof(PlayerData), "InventorySlotMax", MethodType.Getter)]
    [HarmonyPrefix]
    public static bool GetInventorySlotMax(ref int __result) {
        __result = 999999;
        return false;
    }
}