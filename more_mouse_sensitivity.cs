// #name 2x mouse sensitivity
// #author ghorsington
// #desc Increases the maximum mouse sensitivity by 2x.

using BepInEx.Harmony;
using HarmonyLib;
using UnityEngine;
using AIProject;
using System;

public static class WiderMouseSensitivity {
    public static void Main() {
        HarmonyWrapper.PatchAll(typeof(WiderMouseSensitivity));
    }

    [HarmonyPatch(typeof(LocomotionProfile), "CameraPowX", MethodType.Getter)]
    [HarmonyPostfix]
    public static void GetCameraPowX(ref Threshold __result) {
        __result.max *= 2f;
    }

    [HarmonyPatch(typeof(LocomotionProfile), "CameraPowY", MethodType.Getter)]
    [HarmonyPostfix]
    public static void GetCameraPowY(ref Threshold __result) {
        __result.max *= 2f;
    }
}