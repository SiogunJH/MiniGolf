using UnityEngine;
using MessageBoxLib;
using UnityEngine.SceneManagement;
using UtilityLib;

namespace CourseManagerLib
{
    public partial class CourseManager : MonoBehaviour
    {
        // Current World, Level and Level Status
        public static int currentLevelID = 1;
        public static int currentSectionID = 1;
        public static LevelStatus levelStatus;

        // Level Completion/Failure Menu
        private static GameObject levelCompletionMenu;
        private static GameObject levelFailureMenu;


        public static Vector3 GetStartingPoint()
        {
            // Find level object
            Transform levelObject = GameObject.FindGameObjectWithTag("Terrain").transform.Find($"Level {currentSectionID}-{currentLevelID}");

            //Check if the levelObject exist
            if (levelObject is null)
            {
                Debug.LogError($"Level {currentSectionID}-{currentLevelID} was not found!");
                return Vector3.zero;
            }

            //Return levelObject's starting position
            return levelObject.transform.Find("Tee").transform.position + new Vector3(0, 0.5f, 0);
        }
        public static void RestartLevel()
        {
            //Resume gameplay
            UnpauseLevel();
            CloseLevelCompletionMenu();

            //Stop
            golfBall.StopMovement();
            golfBall.StopRotation();
            golfBall.ResetArrow();

            //Set position
            golfBall.transform.position = GetStartingPoint();

            //Prepare for hit
            golfBall.Status = BallStatus.AwaitingHit;
            golfBall.EnableArrow();
        }

        public static void NextLevel()
        {
            //Go to the next level
            currentLevelID++;

            //Set next level position as current
            golfBall.GoTo(GetStartingPoint());
            golfBall.lastPos = golfBall.transform.position;
            golfBall.ResetArrow();

            //Close Completion Menu
            CloseLevelCompletionMenu();
        }

        public static void OpenLevelCompletionMenu()
        {
            // Set statuses
            CourseManager.levelStatus = LevelStatus.Completed;
            golfBall.Status = BallStatus.Disabled;

            // Show level completion menu
            levelCompletionMenu.gameObject.SetActive(true);

            //Show cursor
            Utility.ShowCursor();
        }

        public static void CloseLevelCompletionMenu()
        {
            // Set level status
            CourseManager.levelStatus = LevelStatus.Ongoing;
            golfBall.Status = BallStatus.Moving;

            // Show level completion menu
            levelCompletionMenu.gameObject.SetActive(false);

            //Show cursor
            Utility.HideCursor();
        }
    }
}