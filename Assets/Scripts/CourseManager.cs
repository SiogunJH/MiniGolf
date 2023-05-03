using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CourseManager : MonoBehaviour
{
    #region Gravity
    private const float gravityX = 0.0f;
    private const float gravityY = -20.0f;
    private const float gravityZ = 0.0f;
    #endregion

    private Rigidbody golfBallRb;

    void Start()
    {
        Physics.gravity = new Vector3(gravityX, gravityY, gravityZ);
        golfBallRb = GameObject.FindWithTag("Player").GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            golfBallRb.velocity = new Vector3(-50, 20, 0);
        }
    }
}
