using System;
using EmotesMod.Modules.Components;
using HarmonyLib;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace EmotesMod.Patches;

[HarmonyPatch]
public class HudManagerPatches
{
    public static GameObject EmoteCanvas;
    
    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
    [HarmonyPostfix]
    public static void HudManager_Update_Postfix(HudManager __instance)
    {
        if (EmoteCanvas == null) return;

        if (Input.GetKeyDown(KeyCode.E) && Input.GetKey(KeyCode.LeftControl))
        {
            var wheel = EmoteCanvas.transform.GetChild(0).gameObject;
            wheel.SetActive(!wheel.activeSelf);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            PlayerControl.LocalPlayer.RpcStopEmote();
        }
    }
    
    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Start))]
    [HarmonyPostfix]
    public static void HudManager_Start_Postfix(HudManager __instance)
    {
        EmoteCanvas = Object.Instantiate(Assets.EmoteCanvas);
        EmoteCanvas.transform.GetChild(0).gameObject.SetActive(false);
        EmoteCanvas.transform.GetChild(1).gameObject.SetActive(false);
        EmoteCanvas.transform.GetChild(1).GetChild(2).GetComponent<Button>().onClick.AddListener(new Action(() =>
        {
            PlayerControl.LocalPlayer.RpcStopEmote();
            EmoteCanvas.transform.GetChild(1).gameObject.SetActive(false);
        }));

        if (!OperatingSystem.IsAndroid())
        {
            CreateButtonAndroid();
        }
    }

    public static void CreateButtonAndroid()
    {
        var Button = UnityEngine.Object
            .Instantiate(HudManager.Instance.SettingsButton, HudManager.Instance.SettingsButton.transform.parent)
            .GetComponent<PassiveButton>();
        Button.gameObject.name = "EmoteButton";
        Button.OnClick = new();
        Button.OnClick.AddListener(new Action(() =>
        {
            var wheel = EmoteCanvas.transform.GetChild(0).gameObject;
            wheel.SetActive(!wheel.activeSelf);
        }));
        Button.activeSprites.GetComponent<SpriteRenderer>().sprite = Assets.EmoteButtonHover;
        Button.inactiveSprites.GetComponent<SpriteRenderer>().sprite = Assets.EmoteButton;
        Button.GetComponent<AspectPosition>().DistanceFromEdge = new(4.75f, 0.505f, -400f);
    }
}