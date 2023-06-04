using UnityEngine;
using MessageBoxLib;
using UnityEngine.SceneManagement;

namespace CourseManagerLib
{
    public partial class CourseManager : MonoBehaviour
    {
        // References
        private GolfBall golfBall;
        private Vector3 golfBallVelocity;
        private BallStatus golfBallStatus;

        // Variables
        private bool isPaused = false;
        void PauseLevel()
        {
            // Golf Ball
            if (!isPaused) // On Pause
            {
                //Save data
                golfBallVelocity = golfBall.Rb.velocity;
                golfBallStatus = golfBall.Status;

                //Stop movement
                golfBall.Rb.useGravity = false;
                golfBall.Rb.velocity = Vector3.zero;
                golfBall.Status = BallStatus.Disabled;
            }
            else // On Unpause
            {
                golfBall.Rb.useGravity = true;
                golfBall.Rb.velocity = golfBallVelocity;
                golfBall.Status = golfBallStatus;
            }

            // Change pause status
            isPaused = !isPaused;
        }

    }
}