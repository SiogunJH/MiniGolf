using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{

    private GameObject golfBall;
    [SerializeField] private float rotX = 50;
    [SerializeField] private float rotY = 0;
    [SerializeField] private float rotZ = 0;
    [SerializeField] private float posX;
    [SerializeField] private float posY;
    [SerializeField] private float posZ;
    [SerializeField] private float radius = 10;
    private float newRadius;

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
        rotY = (rotY + Input.GetAxis("Mouse X"));
        rotY += rotY < 0 ? 360 : rotY >= 360 ? -360 : 0; // restrict to range <0:360) and wrap around
        rotX = (rotX - Input.GetAxis("Mouse Y"));
        rotX = rotX < 10 ? 10 : rotX > 80 ? 80 : rotX; // restrict to range <0:360) but dont wrap

        transform.rotation = Quaternion.Euler(rotX, rotY, rotZ);

        // UPDATE POSITION as SECOND and BASE IT AROUND ROTATION
        newRadius = radius * Mathf.Cos((transform.eulerAngles.x) / 180 * Mathf.PI);

        posX = golfBall.transform.position.x + newRadius * Mathf.Sin((-transform.eulerAngles.y) / 180 * Mathf.PI);
        posZ = golfBall.transform.position.z + newRadius * Mathf.Cos((transform.eulerAngles.y + 180) / 180 * Mathf.PI);
        posY = golfBall.transform.position.y + radius * Mathf.Sin((transform.eulerAngles.x) / 180 * Mathf.PI);

        transform.position = new Vector3(posX, posY, posZ);
    }
}
