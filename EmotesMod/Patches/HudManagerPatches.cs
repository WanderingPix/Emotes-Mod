using System;
using EmotesMod.Modules.Components;
using HarmonyLib;
using Rewired;
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

        if (/*ReInput.players.GetPlayer(0).GetButtonDown(InputPatches.OpenEmoteWheelBind.id)*/ Input.GetKeyDown(KeyCode.E) && Input.GetKey(KeyCode.LeftControl) && PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead)
        {
            var wheel = EmoteCanvas.transform.GetChild(0).gameObject;
            wheel.SetActive(!wheel.activeSelf);
        }

        if (/*ReInput.players.GetPlayer(0).GetButtonDown(InputPatches.StopEmotingBind.id)*/ Input.GetKeyDown(KeyCode.X)&& PlayerControl.LocalPlayer.GetComponent<EmoteBehaviour>().currentEmote)
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
        EmoteCanvas.transform.GetChild(1).GetChild(1).GetComponent<Button>().onClick.AddListener(new Action(() =>
        {
            PlayerControl.LocalPlayer.RpcStopEmote();
            EmoteCanvas.transform.GetChild(1).gameObject.SetActive(false);
        }));

        if (OperatingSystem.IsAndroid())
        {
            CreateButtonAndroid();
        }
        else
        {
            __instance.Chat.AddChatWarning("<size=150%>Thanks for downloading Emotes Mod!</size>\nTo open the emote wheel, press Control + E!");
        }
    }

    public static void CreateButtonAndroid()
    {
        var button = UnityEngine.Object
            .Instantiate(HudManager.Instance.SettingsButton, HudManager.Instance.SettingsButton.transform.parent)
            .GetComponent<PassiveButton>();
        button.gameObject.name = "EmoteButton";
        button.OnClick = new();
        button.OnClick.AddListener(new Action(() =>
        {
            var wheel = EmoteCanvas.transform.GetChild(0).gameObject;
            wheel.SetActive(!wheel.activeSelf);
        }));
        button.activeSprites.GetComponent<SpriteRenderer>().sprite = Assets.EmoteButtonHover;
        button.inactiveSprites.GetComponent<SpriteRenderer>().sprite = Assets.EmoteButton;
        button.GetComponent<AspectPosition>().DistanceFromEdge = new(4.75f, 0.505f, -400f);
    }
}