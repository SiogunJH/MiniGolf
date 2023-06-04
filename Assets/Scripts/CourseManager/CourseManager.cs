using UnityEngine;
using MessageBoxLib;
using UnityEngine.SceneManagement;

namespace CourseManagerLib
{
    public partial class CourseManager : MonoBehaviour
    {
        void Update()
        {
            if (Input.GetKeyDown(KeyBindsSettings.KeyBinds[KeyAction.PauseLevel]))
            {
                PauseLevel();
            }
        }

    }
}