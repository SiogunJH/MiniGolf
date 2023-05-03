using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private GameObject golfBall;

    private const float offsetX = 0;
    private const float offsetY = 6;
    private const float offsetZ = -18;

    void Start()
    {
        golfBall = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        float newX = golfBall.transform.position.x + offsetX;
        float newY = golfBall.transform.position.y + offsetY;
        float newZ = golfBall.transform.position.z + offsetZ;
        transform.position = new Vector3(newX, newY, newZ);
    }
}
