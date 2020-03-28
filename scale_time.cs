// #name Change time scale
// #author ghorsington
// #desc Allows the player to speed up or slow down the time with Numpad+ and Numpad-. Numpad* resets the time scale to default.

using BepInEx.Harmony;
using HarmonyLib;
using UnityEngine;
using System.Reflection;
using AIProject.Scene;
using AIProject.UI;
using AIProject;
using UnityEngine.Events;
using System.Collections.Generic;

public static class ScaleTime {
    static Harmony instance;

    public static void Main() {
        instance = HarmonyWrapper.PatchAll(typeof(ScaleTime));
    }

    public static void Unload() {
        instance.UnpatchAll(instance.Id);
        instance = null;
    }

    private static void OnKeyDown(List<AIProject.UI.ICommandData> commands, KeyCode key, UnityAction handler) {
        var kc = new KeyCodeDownCommand { KeyCode = key };
        kc.TriggerEvent.AddListener(handler);
        commands.Add(kc);
    }

    private static void ScaleTimeBy(float amount) {
        Time.timeScale *= amount;
        Singleton<Manager.Map>.Instance.EnvironmentProfile.DayLengthInMinute /= amount;
        MapUIContainer.AddNotify("Game speed set to x" + Time.timeScale.ToString("N"));
    }

    [HarmonyPatch(typeof(MapScene), "InitShortCutEvents")]
    [HarmonyPostfix]
    public static void InitShortCutEvents(ref List<AIProject.UI.ICommandData> ____systemCommands) {
        OnKeyDown(____systemCommands, KeyCode.KeypadPlus, () => ScaleTimeBy(2f));
        OnKeyDown(____systemCommands, KeyCode.KeypadMinus, () => ScaleTimeBy(0.5f));
        OnKeyDown(____systemCommands, KeyCode.KeypadMultiply, () => {
            Time.timeScale = 1f;
            Singleton<Manager.Map>.Instance.EnvironmentProfile.DayLengthInMinute = 40f;
            MapUIContainer.AddNotify("Game speed restored.");
        });
    }
}