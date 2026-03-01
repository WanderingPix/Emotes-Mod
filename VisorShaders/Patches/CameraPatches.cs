using HarmonyLib;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace VisorShaders.Patches;

[HarmonyPatch]
public class CameraPatches
{
    [HarmonyPatch(typeof(SceneManager), nameof(SceneManager.LoadScene))]
    [HarmonyPostfix]
    public static void SceneManager_LoadScene_Postfix()
    {
        foreach (var camera in Camera.allCameras)
        {
            camera.gameObject.AddComponent<CameraFXComponent>();
        }
    }
}