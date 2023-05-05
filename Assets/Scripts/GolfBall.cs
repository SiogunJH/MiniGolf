using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GolfBall : MonoBehaviour
{
    // Objects and Components variables
    public Collider golfBallCol;
    public Rigidbody golfBallRb;
    public PowerMeter powerMeter;
    public Arrow arrow;

    // Other variables
    private List<TerrainType> currentlyColliding;
    private BallStatus golfBallStatus;
    private float powerMeterSpeed;

    void Start()
    {
        // Define variables
        golfBallCol = GetComponent<Collider>();
        golfBallRb = GetComponent<Rigidbody>();
        currentlyColliding = new();
        arrow = GameObject.FindGameObjectWithTag("Arrow").GetComponent<Arrow>();
        powerMeter = GameObject.FindGameObjectWithTag("Power Meter").GetComponent<PowerMeter>();
        golfBallStatus = BallStatus.AwaitingHit;
        powerMeterSpeed = 100.0f;

        // Set bounciness
        if (golfBallCol != null) golfBallCol.material.bounciness = 1.0f;
        else Debug.LogError("GameObject has no collider!");
    }

    void Update()
    {
        if (golfBallStatus == BallStatus.Moving)
        {
            DeaccelerateGolfBall();
        }
        else if (golfBallStatus == BallStatus.AwaitingHit && Input.GetKey(KeyCode.Space))
        {
            powerMeter.sliderValue = powerMeter.sliderValue + Time.deltaTime * powerMeterSpeed;
        }
        else if (golfBallStatus == BallStatus.AwaitingHit && Input.GetKeyUp(KeyCode.Space))
        {
            Hit(powerMeter.sliderValue);
            powerMeter.sliderValue = 0;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // If the collider is of Terrain type, add it's type to the list
        if (collision.gameObject.CompareTag("Terrain"))
            currentlyColliding.Add(collision.gameObject.GetComponent<TerrainCollision>().terrainType);
    }

    void OnCollisionExit(Collision collision)
    {
        // If the collider is of Terrain type, remove it's type from the list
        if (collision.gameObject.CompareTag("Terrain"))
            currentlyColliding.Remove(collision.gameObject.GetComponent<TerrainCollision>().terrainType);
    }

    void DeaccelerateGolfBall()
    {
        // Apply friction
        if (currentlyColliding.Contains(TerrainType.Sand))
        {
            golfBallRb.drag = 6.0f;
            //golfBallRb.angularDrag = 6.0f;
        }
        else if (currentlyColliding.Contains(TerrainType.Grass))
        {
            golfBallRb.drag = 0.8f;
            //golfBallRb.angularDrag = 1.0f;
        }
        else
        {
            golfBallRb.drag = 0.1f;
            //golfBallRb.angularDrag = 0.1f;
        }

        // Check if still
        if (golfBallRb.velocity.magnitude < 0.25f)
        {
            golfBallRb.velocity = new Vector3(0, 0, 0);
            golfBallRb.angularVelocity = new Vector3(0, 0, 0);
            golfBallStatus = BallStatus.AwaitingHit;
            EnableArrow();
        }
    }

    void Hit(float strength)
    {
        float forceX = strength * (1 - arrow.angleV / 90) * Mathf.Sin(arrow.angleH / 180 * Mathf.PI);
        float forceY = strength * (arrow.angleV / 90);
        float forceZ = strength * (1 - arrow.angleV / 90) * Mathf.Cos(arrow.angleH / 180 * Mathf.PI);

        golfBallRb.AddForce(forceX, forceY, forceZ, ForceMode.Impulse);
        golfBallRb.AddTorque(forceZ, 0, -forceX, ForceMode.Impulse);

        golfBallStatus = BallStatus.Moving;
        DisableArrow();
    }

    void DisableArrow()
    {
        arrow.gameObject.SetActive(false);
    }
    void EnableArrow()
    {
        arrow.gameObject.SetActive(true);
    }
}
