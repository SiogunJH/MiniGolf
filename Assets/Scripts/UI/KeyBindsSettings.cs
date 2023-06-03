using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBindsSettings : MonoBehaviour
{
    // Class variables and references
    public static Dictionary<KeyAction, KeyCode> KeyBinds = new();

    // Make KeyBindsSetting accessible through different scenes
    private static KeyBindsSettings Instance;

    private void Awake()
    {
        // If exists
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // One time funtions
        Instance = this;
        DontDestroyOnLoad(gameObject);
        DefaultKeyBinds();
    }

    // Set Keybinds to default
    public void DefaultKeyBinds()
    {
        KeyBinds.Clear();
        KeyBinds.Add(KeyAction.LoadPowerMeter, KeyCode.Space);
    }
}
