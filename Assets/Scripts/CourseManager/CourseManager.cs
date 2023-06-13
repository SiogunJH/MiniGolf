using UnityEngine;
using MessageBoxLib;
using UnityEngine.SceneManagement;

namespace CourseManagerLib
{
    public partial class CourseManager : MonoBehaviour
    {
        void Update()
        {
            if (Input.GetKeyDown(KeyBindsManager.KeyBinds[KeyAction.PauseLevel])) TogglePauseLevel();

        }

    }
}