/*using System;
using HarmonyLib;
using Rewired;
using Rewired.Data;

namespace EmotesMod.Patches;

[HarmonyPatch]
public class InputPatches
{
    private static bool init = false;

    public static InputAction OpenEmoteWheelBind;
    public static InputAction StopEmotingBind;
    public static InputAction SwitchPageUpBind;
    public static InputAction SwitchPageDownBind;
    [HarmonyPatch(typeof(InputManager_Base), nameof(InputManager_Base.Awake))]
    [HarmonyPostfix]
    public static void InputManager_Base_OnInitialized_Postfix(InputManager_Base __instance)
    {
        if (init) return;

        OpenEmoteWheelBind = RegisterModBind(__instance.userData, "emotemod_OpenEmoteWheel", "Open Emote Wheel", "Emotes Mod", KeyboardKeyCode.E, 1, 255, InputActionType.Button, new []
        {
            ModifierKey.Control
        });

        StopEmotingBind = RegisterModBind(__instance.userData, "emotemod_StopEmoting", "Stop Emoting", "Emotes Mod",
            KeyboardKeyCode.X, 0, 254);
        
        SwitchPageUpBind = RegisterModBind(__instance.userData, "emotemod_SwitchPageUp", "Emote Wheel Page Up", "Emotes Mod",
            KeyboardKeyCode.RightArrow, 0, 253);
        
        SwitchPageDownBind = RegisterModBind(__instance.userData, "emotemod_SwitchPageDown", "Emote Wheel Page Down", "Emotes Mod",
            KeyboardKeyCode.LeftArrow, 0, 252);

        init = true;
    }
    
    //Taken from MiraAPI.
    
    /// <summary>
    /// Registers a new mod keybind as a user-assignable button action in Rewired.
    /// </summary>
    /// <param name="userData">The Rewired user data to add the action to.</param>
    /// <param name="id">The internal name of the action.</param>
    /// <param name="name">Text shown in the rebinding UI.</param>
    /// /// <param name="group">Group shown above the label.</param>
    /// <param name="key">The default key to assign to this action.</param>
    /// <param name="category">Category ID to group actions in Rewired (default is 0).</param>
    /// <param name="elementIdentifierId">The element identifier ID (default is -1, meaning none specified).</param>
    /// <param name="type">The <see cref="InputActionType"/> for this action (default is Button).</param>
    /// <param name="modifiers">Optional modifier keys (e.g., <c>Control</c>, <c>Shift</c>, <c>Alt</c>) that must be held together with the main key.</param>
    /// <returns>The action ID of the newly registered action.</returns>
    public static InputAction RegisterModBind(UserData userData, string id, string name, string? group, KeyboardKeyCode key, int category = 0, int elementIdentifierId = -1, InputActionType type = InputActionType.Button, ModifierKey[]? modifiers = null)
    {
        userData.AddAction(category);
        var action = userData.GetAction(userData.actions.Count - 1)!;
        
        action.name = id;
        action.descriptiveName = group != null
            ? $"<b><size=70%>{Palette.CrewmateRoleHeaderDarkBlue.ToTextColor()}{group.ReplaceLineEndings(" ")}</color></size></b>\n{name}"
            : name;
        action.categoryId = category;
        action.type = type;
        action.userAssignable = true;

        var map = new ActionElementMap
        {
            _elementIdentifierId = elementIdentifierId,
            _actionId = action.id,
            _elementType = ControllerElementType.Button,
            _axisContribution = Pole.Positive,
            _keyboardKeyCode = key,
        };

        if (modifiers != null)
        {
            if (modifiers.Length > 0) map._modifierKey1 = modifiers[0];
            if (modifiers.Length > 1) map._modifierKey2 = modifiers[1];
            if (modifiers.Length > 2) map._modifierKey3 = modifiers[2];
        }

        userData.keyboardMaps[0].actionElementMaps.Add(map);
        userData.joystickMaps[0].actionElementMaps.Add(map);
        return action;
    }
}*/