using UnityEngine;
using MessageBoxLib;
using UnityEngine.SceneManagement;

namespace CourseManagerLib
{
    public partial class CourseManager : MonoBehaviour
    {
        public static int currentLevelID = 1;
        public static int currentSectionID = 1;
        public static Vector3 GetStartingPoint()
        {
            // Find level object
            Transform levelObject = GameObject.FindGameObjectWithTag("Terrain").transform.Find($"Level {currentSectionID}-{currentLevelID}");

            //Check if the levelObject exist
            if (levelObject is null)
            {
                Debug.LogError($"Level {currentSectionID}-{currentLevelID} was not found!");
                return new Vector3();
            }

            //Return levelObject's starting position
            return levelObject.transform.Find("Starting Point").transform.position;
        }
        public static void RestartLevel()
        {
            //Resume gameplay
            UnpauseLevel();

            //Stop
            golfBall.StopMovement();
            golfBall.StopRotation();

            //Set position
            golfBall.transform.position = CourseManager.GetStartingPoint();

            //Prepare for hit
            golfBall.Status = BallStatus.AwaitingHit;
            golfBall.EnableArrow();
        }
    }
}