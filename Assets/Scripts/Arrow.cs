using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private GameObject golfBall;
    public float angleH = 0;
    public float angleV = 0;
    private float currentBounce = 0;
    private const float constBounce = 0.1f;

    void Start()
    {
        golfBall = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.A))
            angleH -= 90 * Time.deltaTime;

        if (Input.GetKey(KeyCode.D))
            angleH += 90 * Time.deltaTime;

        if (Input.GetKey(KeyCode.W))
            angleV += 60 * Time.deltaTime;

        if (Input.GetKey(KeyCode.S))
            angleV -= 60 * Time.deltaTime;

        angleH += angleH < 0 ? 360 : angleH >= 360 ? -360 : 0; // restrict to range <0:360) and wrap around
        angleV = angleV < 0 ? 0 : angleV > 70 ? 70 : angleV; // restrict to range <0:70)

        currentBounce = (currentBounce + 180 * Time.deltaTime) % 360;
    }

    void LateUpdate()
    {
        transform.rotation = Quaternion.Euler(-angleV, angleH, 0);

        float angleVFraction = angleV / 90.0f;
        float bounce = 1 + constBounce * Mathf.Sin(currentBounce / 180 * Mathf.PI);

        float offsetX = bounce * 2.5f * (1 - angleVFraction) * Mathf.Sin(angleH / 180 * Mathf.PI);
        float offsetZ = bounce * 2.5f * (1 - angleVFraction) * Mathf.Cos(angleH / 180 * Mathf.PI);
        float offsetY = bounce * 2.5f * angleVFraction;

        float newX = golfBall.transform.position.x + offsetX;
        float newZ = golfBall.transform.position.z + offsetZ;
        float newY = golfBall.transform.position.y + offsetY;

        transform.position = new Vector3(newX, newY, newZ);
    }
}
