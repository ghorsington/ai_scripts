// #name Item cheats
// #author ghorsington
// #desc Allows to craft any items and build any props. Press F7 to give yourself 999 of every in-game item.

using BepInEx.Harmony;
using HarmonyLib;
using UnityEngine;
using AIProject.UI;
using AIProject;
using AIProject.Scene;
using AIProject.SaveData;
using AIProject.UI.Viewer;
using Housing.Add;
using System;
using System.Collections.Generic;
using UnityEngine.Events;

public static class SkipResources
{
	
	static HarmonyLib.Harmony instance;
	
    public static void Main() {
        HarmonyWrapper.PatchAll(typeof(SkipResources), instance = new HarmonyLib.Harmony(nameof(SkipResources)));
		instance.Patch(typeof(Manager.Housing.LoadInfo).GetConstructor(new[] { typeof(int), typeof(List<string>) }), postfix: new HarmonyMethod(typeof(SkipResources).GetMethod("LoadInfoCtor")));
    }

    public static void Unload()
    {
        instance.UnpatchAll(instance.Id);
        instance = null;
    }

    private static void OnKeyDown(List<AIProject.UI.ICommandData> commands, KeyCode key, UnityAction handler)
    {
        var kc = new KeyCodeDownCommand { KeyCode = key };
        kc.TriggerEvent.AddListener(handler);
        commands.Add(kc);
    }

    [HarmonyPatch(typeof(MapScene), "InitShortCutEvents")]
    [HarmonyPostfix]
    public static void InitShortCutEvents(ref List<AIProject.UI.ICommandData> ____systemCommands)
    {
        OnKeyDown(____systemCommands, KeyCode.F7, () => {
            PlayerActor player = Singleton<Manager.Map>.Instance.Player;
            Manager.Resources.GameInfoTables gameInfo = Singleton<Manager.Resources>.Instance.GameInfo;

            if (player == null || gameInfo == null)
                return;

            foreach (int category in gameInfo.GetItemCategories())
                foreach (StuffItemInfo stuffItemInfo in gameInfo.GetItemTable(category).Values)
                    player.PlayerData.ItemList.Add(new StuffItem(stuffItemInfo.CategoryID, stuffItemInfo.ID, 999));
            MapUIContainer.AddNotify("All items are added to your inventory");
        });

        OnKeyDown(____systemCommands, KeyCode.F8, () => {
            PlayerActor player = Singleton<Manager.Map>.Instance.Player;
            if (player != null && player.PlayerData.ItemList.Count > 0)
            {
                player.PlayerData.ItemList.Clear();
                MapUIContainer.AddNotify("Your inventory has been cleared.");
            }
        });
    }

    [HarmonyPatch(typeof(CraftUI), "Possible")]
    [HarmonyPrefix]
    public static bool CheckPossible(ref RecipeDataInfo[] __result, RecipeDataInfo[] info)
    {
        __result = info;
        return false;
    }

    [HarmonyPatch(typeof(CraftViewer), "Possible")]
    [HarmonyPrefix]
    public static bool CheckPossible(ref CraftItemNodeUI.Possible __result)
    {
        __result = new CraftItemNodeUI.Possible(false, true);
        return false;
    }

    [HarmonyPatch(typeof(RecipeItemNodeUI), "ItemCount", MethodType.Getter)]
    [HarmonyPrefix]
    public static bool GetCraftItemUIItemCount(ref int __result)
    {
        __result = 6969;
        return false;
    }

    public static void LoadInfoCtor(Manager.Housing.LoadInfo __instance)
    {
        __instance.requiredMaterials = new Manager.Housing.RequiredMaterial[0];
    }
}