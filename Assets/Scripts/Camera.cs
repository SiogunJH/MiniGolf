using UnityEngine;
using UtilityLib;

namespace CameraLib
{
    public class Camera : MonoBehaviour
    {
        // References
        private GolfBall golfBall;

        // Variables
        private Quaternion rot = new(50, 0, 0, 0);
        private Vector3 pos = new();
        private float radius = 10;
        private float newRadius;

        void Start()
        {
            // Set variables
            golfBall = GameObject.FindWithTag("Golf Ball").gameObject.GetComponent<GolfBall>();
        }

        void LateUpdate()
        {
            if (golfBall.Status == BallStatus.Disabled) return;

            // UPDATE ROTATION
            rot.y = (rot.y + Input.GetAxis("Mouse X"));
            rot.y.RestrictBetween(0, 360, true);
            rot.x = (rot.x - Input.GetAxis("Mouse Y"));
            rot.x.RestrictBetween(5, 90, false);
            transform.rotation = Quaternion.Euler(rot.x, rot.y, rot.z);

            // UPDATE POSITION
            newRadius = radius * Mathf.Cos((transform.eulerAngles.x).ToRadians());
            pos.x = golfBall.transform.position.x + newRadius * Mathf.Sin((-transform.eulerAngles.y).ToRadians());
            pos.z = golfBall.transform.position.z + newRadius * Mathf.Cos((transform.eulerAngles.y + 180).ToRadians());
            pos.y = golfBall.transform.position.y + radius * Mathf.Sin((transform.eulerAngles.x).ToRadians());
            transform.position = pos;
        }
    }
}
