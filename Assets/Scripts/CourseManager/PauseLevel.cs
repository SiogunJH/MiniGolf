using UnityEngine;
using MessageBoxLib;
using UnityEngine.SceneManagement;

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

        // Variables
        public static bool isPaused = false;

        public static void TogglePauseLevel()
        {
            // Switch state
            isPaused = !isPaused;

            // Act accordingly
            if (isPaused) PauseLevel();
            else UnpauseLevel();
        }

        public static void UnpauseLevel()
        {
            // Golf Ball
            golfBall.Rb.useGravity = true;
            golfBall.Rb.velocity = golfBallVelocity;
            golfBall.Rb.angularVelocity = golfBallAngularVelocity;
            golfBall.Status = golfBallStatus;

            // Cursor
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            // Pause Menu
            pauseMenu.gameObject.SetActive(false);
            isPaused = false;
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
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            // Pause Menu
            pauseMenu.gameObject.SetActive(true);
            isPaused = true;
        }
    }
}