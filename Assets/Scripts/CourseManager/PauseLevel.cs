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
        private static bool isPaused = false;
        public static void PauseLevel()
        {
            // Change pause status
            isPaused = !isPaused;

            if (isPaused) // On Pause
            {
                // Golf Ball
                golfBallVelocity = golfBall.Rb.velocity;
                golfBallAngularVelocity = golfBall.Rb.angularVelocity;
                golfBallStatus = golfBall.Status;
                golfBall.Rb.useGravity = false;
                golfBall.Rb.velocity = Vector3.zero;
                golfBall.Rb.angularVelocity = Vector3.zero;
                golfBall.Status = BallStatus.Disabled;

                // Cursor
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                // Pause Menu
                pauseMenu.gameObject.SetActive(true);
            }
            else // On Unpause
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
            }
        }

    }
}