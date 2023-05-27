using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GolfBall : MonoBehaviour
{
    // Golf Ball refs and vars
    [HideInInspector] public BoxCollider golfBallCol;
    [HideInInspector] public Rigidbody golfBallRb;
    private BallStatus golfBallStatus;

    // Power Meter refs and vars
    [HideInInspector] public PowerMeter powerMeter;
    private float powerMeterSpeed;

    void Start()
    {
        // Define GolfBall refs and vars
        golfBallCol = GetComponent<BoxCollider>();
        golfBallRb = GetComponent<Rigidbody>();
        golfBallStatus = BallStatus.AwaitingHit;

        // Define Arrow ref and vars
        arrow = GameObject.FindGameObjectWithTag("Arrow").GetComponent<Arrow>();

        // Define Power Meter refs and vars
        powerMeter = GameObject.FindGameObjectWithTag("Power Meter").GetComponent<PowerMeter>();
        powerMeterSpeed = powerMeter.slider.maxValue * 1.5f;

        // Define other
        terrainCluster = GameObject.FindWithTag("Terrain");
        currentlyColliding = new();
        tryingToStop = false;
    }

    void Update()
    {
        // Apply Friction
        if (golfBallStatus == BallStatus.Moving)
        {
            Deaccelerate();
        }

        // Disable Hit Mechanic if the GolfBall started Moving from being AwaitingHit
        if (golfBallStatus == BallStatus.AwaitingHit && golfBallRb.velocity.magnitude > 0.1f)
        {
            powerMeter.sliderValue = 0;
            golfBallStatus = BallStatus.Moving;
            DisableArrow();
        }

        // Hit Mechanic
        if (golfBallStatus == BallStatus.AwaitingHit && Input.GetKey(KeyCode.Space))
        {
            powerMeter.sliderValue = powerMeter.sliderValue + Time.deltaTime * powerMeterSpeed;
        }
        else if (golfBallStatus == BallStatus.AwaitingHit && Input.GetKeyUp(KeyCode.Space))
        {
            Hit(powerMeter.sliderValue);

            powerMeter.sliderValue = 0;
            golfBallStatus = BallStatus.Moving;
            DisableArrow();
        }

        // --==## TEMP ##==--
        if (Input.GetKeyDown(KeyCode.R))
            GoTo(CourseManager.Instance.GetStartingPoint(CourseManager.Instance.currentLevelID));
    }
}
