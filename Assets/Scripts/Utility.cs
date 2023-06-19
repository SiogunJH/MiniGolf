using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UtilityLib
{
    public static class Utility
    {
        public static float ToRadians(this float degrees) => degrees / 180 * Mathf.PI;
        public static void RestrictBetween(ref this float toRestrict, float min, float max, bool loop = false)
        {
            // Verify syntax
            if (min > max)
                Debug.LogError("Cannot RestrictBetween when minimum value is bigger than maximum value");

            // Restrict
            if (loop) toRestrict += toRestrict < min ? max - min : toRestrict > max ? min - max : 0;
            else toRestrict = toRestrict < min ? min : toRestrict > max ? max : toRestrict;
        }
        public static void ShowCursor()
        {
            // Cursor
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        public static void HideCursor()
        {
            // Cursor
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}