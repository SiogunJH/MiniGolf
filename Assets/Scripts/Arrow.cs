using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace ArrowLib
{
    public class Arrow : MonoBehaviour
    {
        // References
        private GameObject golfBall;

        // Rotation
        [HideInInspector] public float rotY = 0;
        [HideInInspector] public float rotX = 0;

        // Position
        private float posX;
        private float posY;
        private float posZ;
        private float currentBounce = 0;
        private const float constBounce = 0.2f;
        private const float distance = 1f;

        void Start()
        {
            // Define object and reference variables
            golfBall = GameObject.FindGameObjectWithTag("Golf Ball");
        }

        void Update()
        {
            if (Input.GetKey(KeyCode.A)) rotY -= 90 * Time.deltaTime;
            if (Input.GetKey(KeyCode.D)) rotY += 90 * Time.deltaTime;
            if (Input.GetKey(KeyCode.W)) rotX += 60 * Time.deltaTime;
            if (Input.GetKey(KeyCode.S)) rotX -= 60 * Time.deltaTime;

            rotY.RestrictBetween(0, 360, true);
            rotX.RestrictBetween(0, 70, false);

            currentBounce = (currentBounce + 200 * Time.deltaTime) % 360;
        }

        void LateUpdate()
        {
            transform.rotation = Quaternion.Euler(-rotX, rotY, 0);

            // Golf Ball Position + (Bounce Offset) * Rotation Offset
            posX = golfBall.transform.position.x + (distance + constBounce * Mathf.Sin(currentBounce.ToRadians())) * Mathf.Cos(rotX.ToRadians()) * Mathf.Sin(rotY.ToRadians());
            posZ = golfBall.transform.position.z + (distance + constBounce * Mathf.Sin(currentBounce.ToRadians())) * Mathf.Cos(rotX.ToRadians()) * Mathf.Cos(rotY.ToRadians());
            posY = golfBall.transform.position.y + (distance + constBounce * Mathf.Sin(currentBounce.ToRadians())) * Mathf.Sin(rotX.ToRadians());

            transform.position = new Vector3(posX, posY, posZ);
        }
    }
}