using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Arrow : MonoBehaviour
{
    // References
    private GameObject golfBall;
    private MeshRenderer meshRenderer;

    // Rotation
    [HideInInspector] public float angleH = 0;
    [HideInInspector] public float angleV = 0;

    // Position
    private float posX;
    private float posY;
    private float posZ;
    private float currentBounce = 0;
    private const float constBounce = 0.3f;
    private const float distance = 1f;

    void Start()
    {
        // Define object and reference variables
        golfBall = GameObject.FindGameObjectWithTag("Golf Ball");
        meshRenderer = gameObject.GetComponentInChildren<MeshRenderer>();

        // Define other variables

        // Disable shadow casting
        meshRenderer.shadowCastingMode = ShadowCastingMode.Off;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.A)) angleH -= 90 * Time.deltaTime;
        if (Input.GetKey(KeyCode.D)) angleH += 90 * Time.deltaTime;
        if (Input.GetKey(KeyCode.W)) angleV += 60 * Time.deltaTime;
        if (Input.GetKey(KeyCode.S)) angleV -= 60 * Time.deltaTime;

        angleH.RestrictBetween(0, 360, true);
        angleV.RestrictBetween(0, 70, false);

        currentBounce = (currentBounce + 180 * Time.deltaTime) % 360;
    }

    void LateUpdate()
    {
        transform.rotation = Quaternion.Euler(-angleV, angleH, 0);

        float bounce = distance + constBounce * Mathf.Sin(currentBounce.ToRadians());

        float offsetX = bounce * (Mathf.Cos(angleV.ToRadians())) * Mathf.Sin(angleH.ToRadians());
        float offsetZ = bounce * (Mathf.Cos(angleV.ToRadians())) * Mathf.Cos(angleH.ToRadians());
        float offsetY = bounce * angleV / 90;

        posX = golfBall.transform.position.x + offsetX;
        posZ = golfBall.transform.position.z + offsetZ;
        posY = golfBall.transform.position.y + offsetY;

        transform.position = new Vector3(posX, posY, posZ);
    }
}
