using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using CourseManagerLib;

public class MainMenuActions : MonoBehaviour
{
    [SerializeField] private GameObject MainMenu;
    [SerializeField] private GameObject SectionSelection;
    [SerializeField] private GameObject LevelSelection;

    public void Settings()
    {
        Debug.Log("Settings Button");
    }

    public void QuitApp()
    {
        Application.Quit();
    }

    public void GoToMainMenu()
    {
        LevelSelection.SetActive(false);
        SectionSelection.SetActive(false);
        MainMenu.SetActive(true);
    }

    public void GoToSectionSelection()
    {
        LevelSelection.SetActive(false);
        MainMenu.SetActive(false);
        SectionSelection.SetActive(true);
    }

    public void GoToLevelSection(int sectionID)
    {
        MainMenu.SetActive(false);
        SectionSelection.SetActive(false);
        LevelSelection.SetActive(true);
        CourseManager.currentSectionID = sectionID;
    }

    public void PlayLevel(int levelID)
    {
        CourseManager.currentLevelID = levelID;
        SceneManager.LoadScene("Course");
    }
}
