using UnityEngine;
using MessageBoxLib;
using UnityEngine.SceneManagement;

namespace CourseManagerLib
{
    public partial class CourseManager : MonoBehaviour
    {
        public static int currentLevelID = 1;
        public static int currentSectionID = 1;
        public static Vector3 GetStartingPoint(int levelID)
        {
            switch (levelID)
            {
                case 1:
                    return GameObject.FindGameObjectWithTag("Terrain").transform.Find("Level 1-1").transform.Find("Starting Point").transform.position;
                case 2:
                    return GameObject.FindGameObjectWithTag("Terrain").transform.Find("Level 1-2").transform.Find("Starting Point").transform.position;
            }

            Debug.LogError($"Level {levelID} was not found!");
            return new Vector3();
        }
        public static void RestartLevel()
        {
            //Resume gameplay
            UnpauseLevel();

            //Stop
            golfBall.StopMovement();
            golfBall.StopRotation();

            //Set position
            golfBall.transform.position = CourseManager.GetStartingPoint(CourseManager.currentLevelID);
        }
    }
}