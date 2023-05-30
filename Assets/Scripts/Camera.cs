using UnityEngine;

namespace CameraLib
{
    public class Camera : MonoBehaviour
    {
        // Other Variables
        private GameObject golfBall;

        // Rotation
        [SerializeField] private float rotX = 50;
        [SerializeField] private float rotY = 0;
        [SerializeField] private float rotZ = 0;

        // Position
        [SerializeField] private float posX;
        [SerializeField] private float posY;
        [SerializeField] private float posZ;
        [SerializeField] private float radius = 10;
        [HideInInspector] private float newRadius;

        void Start()
        {
            // Set variables
            golfBall = GameObject.FindGameObjectWithTag("Golf Ball");

            // Cursor behavior
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        void LateUpdate()
        {
            // UPDATE ROTATION
            rotY = (rotY + Input.GetAxis("Mouse X"));
            rotY.RestrictBetween(0, 360, true);
            rotX = (rotX - Input.GetAxis("Mouse Y"));
            rotX.RestrictBetween(5, 90, false);

            transform.rotation = Quaternion.Euler(rotX, rotY, rotZ);

            // UPDATE POSITION based on rotation
            newRadius = radius * Mathf.Cos((transform.eulerAngles.x).ToRadians());

            posX = golfBall.transform.position.x + newRadius * Mathf.Sin((-transform.eulerAngles.y).ToRadians());
            posZ = golfBall.transform.position.z + newRadius * Mathf.Cos((transform.eulerAngles.y + 180).ToRadians());
            posY = golfBall.transform.position.y + radius * Mathf.Sin((transform.eulerAngles.x).ToRadians());

            transform.position = new Vector3(posX, posY, posZ);
        }
    }
}
