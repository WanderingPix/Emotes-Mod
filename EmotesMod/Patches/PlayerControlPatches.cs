using System.Linq;
using EmotesMod.Modules.Components;
using HarmonyLib;
using Rewired;
using UnityEngine;

namespace EmotesMod.Patches;

[HarmonyPatch]
public class PlayerControlPatches
{
    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.Awake))]
    [HarmonyPostfix]
    public static void PlayerControl_Awake_Postfix(PlayerControl __instance)
    {
        __instance.gameObject.AddComponent<EmoteBehaviour>().pc = __instance;
        __instance.MyPhysics.Animations.glowAnimator.gameObject.SetActive(false);
    }
    
    [HarmonyPatch(typeof(PlayerPhysics), nameof(PlayerPhysics.HandleAnimation))]
    [HarmonyPrefix]
    public static bool PlayerPhysics_Awake_Prefix(PlayerPhysics __instance)
    {
        var emoteBehaviour = __instance.GetComponent<EmoteBehaviour>();
        if (emoteBehaviour == null || emoteBehaviour.currentEmote == null) return true;
        if (emoteBehaviour.currentEmote.canMove)
        {
            __instance.FlipX = __instance.Velocity.x <= 0;
            return false;
        }

        return true;
    }
    
    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.Die))]
    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.OnGameEnd))]
    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.OnGameStart))]
    [HarmonyPostfix]
    public static void PlayerControl_Die_Postfix(PlayerControl __instance)
    {
        __instance.gameObject.GetComponent<EmoteBehaviour>().StopEmote();
    }
}