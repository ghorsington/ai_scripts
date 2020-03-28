// #name Always talk to the girls
// #author ghorsington
// #desc Sets talk motivation of the girls to a very big value, allowing you to always talk to them.

using BepInEx.Harmony;
using HarmonyLib;
using AIProject.SaveData;

public static class MaxTalkMotivation {
    static Harmony instance;

    public static void Main() {
        instance = HarmonyWrapper.PatchAll(typeof(MaxTalkMotivation));
    }

    public static void Unload() {
        instance.UnpatchAll(instance.Id);
        instance = null;
    }
    
    [HarmonyPatch(typeof(AgentData), "TalkMotivation", MethodType.Getter)]
    [HarmonyPrefix]
    public static bool TalkMotivationCheck(ref float __result) {
        __result = 1000000f;
        return false;
    }
}