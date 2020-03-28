// #name Quicksave the game with F5
// #author ghorsington
// #desc Allows to quicksave the game by pressing F5.

using BepInEx.Harmony;
using HarmonyLib;
using UnityEngine;
using System.Reflection;
using AIProject.Scene;
using AIProject.UI;
using AIProject;
using UnityEngine.Events;
using System.Collections.Generic;

public static class QuickSave {
    static Harmony instance;

    public static void Main() {
        instance = HarmonyWrapper.PatchAll(typeof(QuickSave));
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

    [HarmonyPatch(typeof(MapScene), "InitShortCutEvents")]
    [HarmonyPostfix]
    public static void InitShortCutEvents(ref List<AIProject.UI.ICommandData> ____systemCommands) {
        OnKeyDown(____systemCommands, KeyCode.F5, () => {
            if(Singleton<MapScene>.IsInstance()) {
                Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Save);
                Singleton<MapScene>.Instance.SaveProfile(true);
                MapUIContainer.AddNotify("Game saved.");
            }
        });
    }
}