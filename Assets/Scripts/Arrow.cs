using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UtilityLib;

namespace ArrowLib
{
    public class Arrow : MonoBehaviour
    {
        // References
        private GameObject golfBall;

        // Variables
        [HideInInspector] public Quaternion rot = new();
        [HideInInspector] public Vector3 pos = new();
        private float currentBounce = 0;
        private const float constBounce = 0.2f;
        private const float distance = 1f;

        void Start()
        {
            // Define object and reference variables
            golfBall = GameObject.FindGameObjectWithTag("Golf Ball");
        }

        void LateUpdate()
        {
            //UPDATE ROTATION
            if (Input.GetKey(KeyCode.A)) rot.y -= 90 * Time.deltaTime;
            if (Input.GetKey(KeyCode.D)) rot.y += 90 * Time.deltaTime;
            if (Input.GetKey(KeyCode.W)) rot.x += 60 * Time.deltaTime;
            if (Input.GetKey(KeyCode.S)) rot.x -= 60 * Time.deltaTime;
            rot.y.RestrictBetween(0, 360, true);
            rot.x.RestrictBetween(0, 70, false);
            transform.rotation = Quaternion.Euler(-rot.x, rot.y, 0);

            //UPDATE POSITION
            currentBounce = (currentBounce + 200 * Time.deltaTime) % 360;
            pos.x = golfBall.transform.position.x + (distance + constBounce * Mathf.Sin(currentBounce.ToRadians())) * Mathf.Cos(rot.x.ToRadians()) * Mathf.Sin(rot.y.ToRadians());
            pos.z = golfBall.transform.position.z + (distance + constBounce * Mathf.Sin(currentBounce.ToRadians())) * Mathf.Cos(rot.x.ToRadians()) * Mathf.Cos(rot.y.ToRadians());
            pos.y = golfBall.transform.position.y + (distance + constBounce * Mathf.Sin(currentBounce.ToRadians())) * Mathf.Sin(rot.x.ToRadians());
            transform.position = pos;
        }
    }
}