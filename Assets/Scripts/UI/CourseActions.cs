using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using CourseManagerLib;

public class CourseActions : MonoBehaviour
{
    [SerializeField] private static GameObject PauseMenu;

    public void Settings()
    {
        Debug.Log("Settings Button");
    }

    public void Quit()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void Restart()
    {
        Debug.Log("Restart Button");
    }

    public static void ClosePauseMenu()
    {
        CourseManager.PauseLevel();
    }
}
