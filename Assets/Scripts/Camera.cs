using CourseManagerLib;
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
        private float zoomSpeed = 10;

        void Start()
        {
            // Set variables
            golfBall = GameObject.FindWithTag("Golf Ball").gameObject.GetComponent<GolfBall>();
        }

        void Update()
        {
            if (CourseManager.levelStatus != LevelStatus.Ongoing) return;

            //Zoom In
            if (Input.GetKey(KeyBindsManager.KeyBinds[KeyAction.ZoomIn]))
            {
                radius -= Time.deltaTime * zoomSpeed;
                radius.RestrictBetween(2, 50);
            }
            else if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                radius -= Time.deltaTime * zoomSpeed * 30;
                radius.RestrictBetween(2, 50);
            }

            //Zoom Out
            if (Input.GetKey(KeyBindsManager.KeyBinds[KeyAction.ZoomOut]))
            {
                radius += Time.deltaTime * zoomSpeed;
                radius.RestrictBetween(2, 50);
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            {
                radius += Time.deltaTime * zoomSpeed * 30;
                radius.RestrictBetween(2, 50);
            }

            //Zoom Reset
            if (Input.GetKey(KeyBindsManager.KeyBinds[KeyAction.ZoomReset]))
            {
                radius = 10;
            }

        }

        void LateUpdate()
        {
            if (CourseManager.levelStatus != LevelStatus.Ongoing) return;

            // UPDATE ROTATION
            rot.y = (rot.y + Input.GetAxis("Mouse X"));
            rot.y.RestrictBetween(0, 360, true);
            rot.x = (rot.x - Input.GetAxis("Mouse Y"));
            rot.x.RestrictBetween(5, 90, false);
            transform.rotation = Quaternion.Euler(rot.x, rot.y, rot.z);

            // UPDATE POSITION
            pos.x = golfBall.transform.position.x + radius * Mathf.Cos((transform.eulerAngles.x).ToRadians()) * Mathf.Sin((-transform.eulerAngles.y).ToRadians());
            pos.z = golfBall.transform.position.z + radius * Mathf.Cos((transform.eulerAngles.x).ToRadians()) * Mathf.Cos((transform.eulerAngles.y + 180).ToRadians());
            pos.y = golfBall.transform.position.y + radius * Mathf.Sin((transform.eulerAngles.x).ToRadians());
            transform.position = pos;
        }
    }
}
