using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private GameObject golfBall;
    [SerializeField] private float horizontal = 0;
    [SerializeField] private float vertical = 50;
    [SerializeField] private const float distance = 20;

    void Start()
    {
        // Set variables
        golfBall = GameObject.FindGameObjectWithTag("Player");

        // Cursor behavior
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        // UPDATE ROTATION as FIRST
        horizontal = (horizontal + Input.GetAxis("Mouse X"));
        horizontal += horizontal < 0 ? 360 : horizontal >= 360 ? -360 : 0; // restrict to range <0:360) and wrap around
        vertical = (vertical - Input.GetAxis("Mouse Y"));
        vertical = vertical < 10 ? 10 : vertical > 80 ? 80 : vertical; // restrict to range <0:360) but dont wrap

        float newRX = vertical;
        float newRZ = 0;
        float newRY = horizontal;

        transform.rotation = Quaternion.Euler(newRX, newRY, newRZ);

        // UPDATE POSITION as SECOND because BASE IT AROUND ROTATION
        float newDistance = distance * Mathf.Cos((transform.eulerAngles.x) / 180 * Mathf.PI);
        float offsetX = newDistance * Mathf.Sin((-transform.eulerAngles.y) / 180 * Mathf.PI);
        float offsetZ = newDistance * Mathf.Cos((transform.eulerAngles.y + 180) / 180 * Mathf.PI);
        float offsetY = distance * Mathf.Sin((transform.eulerAngles.x) / 180 * Mathf.PI);

        float newX = golfBall.transform.position.x + offsetX;
        float newZ = golfBall.transform.position.z + offsetZ;
        float newY = golfBall.transform.position.y + offsetY;

        transform.position = new Vector3(newX, newY, newZ);
    }
}
