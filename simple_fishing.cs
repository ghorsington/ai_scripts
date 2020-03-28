// #name Easy fishing
// #author ghorsington
// #desc Skips the whole luring mini-game during fishing. Now this is almost like fishing in old-school RS!

using BepInEx.Harmony;
using HarmonyLib;
using UnityEngine;
using AIProject.MiniGames.Fishing;
using System.Reflection;

public static class SimpleFishing {
    static Harmony instance;

    public static void Main() {
        instance = HarmonyWrapper.PatchAll(typeof(SimpleFishing));
    }

    public static void Unload() {
        instance.UnpatchAll(instance.Id);
        instance = null;
    }

    [HarmonyPatch(typeof(FishingManager), "CheckArrowInCircle")]
    [HarmonyPrefix]
    public static bool CheckPossible(FishingManager __instance, ref float ___fishHeartPoint) {
        ___fishHeartPoint = 0f;
        __instance.scene = FishingManager.FishingScene.Success;
        __instance.SceneToSuccess();
        return false;
    }
}