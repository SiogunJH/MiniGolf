using UnityEngine;
using MessageBoxLib;
using UnityEngine.SceneManagement;
using UtilityLib;

namespace CourseManagerLib
{
    public partial class CourseManager : MonoBehaviour
    {
        // GolfBall References & Variables
        private static GolfBall golfBall;
        private static Vector3 golfBallVelocity;
        private static Vector3 golfBallAngularVelocity;
        private static BallStatus golfBallStatus;

        //Other
        private static GameObject pauseMenu;

        public static void TogglePauseLevel()
        {
            if (levelStatus == LevelStatus.Paused)
            {
                UnpauseLevel();
            }
            else if (levelStatus == LevelStatus.Ongoing)
            {
                PauseLevel();
            }
        }

        public static void UnpauseLevel()
        {
            // Golf Ball
            golfBall.Rb.useGravity = true;
            golfBall.Rb.velocity = golfBallVelocity;
            golfBall.Rb.angularVelocity = golfBallAngularVelocity;
            golfBall.Status = golfBallStatus;

            // Cursor
            Utility.HideCursor();

            // Pause Menu
            pauseMenu.gameObject.SetActive(false);
            levelStatus = LevelStatus.Ongoing;
        }
        public static void PauseLevel()
        {
            // Golf Ball
            golfBall.Rb.useGravity = false;

            golfBallVelocity = golfBall.StopMovement();
            golfBallAngularVelocity = golfBall.StopRotation();

            golfBallStatus = golfBall.Status;
            golfBall.Status = BallStatus.Disabled;

            // Cursor
            Utility.ShowCursor();

            // Pause Menu
            pauseMenu.gameObject.SetActive(true);
            levelStatus = LevelStatus.Paused;
        }
    }
}