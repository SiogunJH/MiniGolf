using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GolfBall : MonoBehaviour
{
    // Objects and Components variables
    public BoxCollider golfBallCol;
    public Rigidbody golfBallRb;
    public PowerMeter powerMeter;
    public Arrow arrow;
    public GameObject terrainCluster;

    // Other variables
    private List<TerrainType> currentlyColliding;
    private BallStatus golfBallStatus;
    private bool tryingToStop;
    private float powerMeterSpeed;

    void Start()
    {
        // Define object component references
        golfBallCol = GetComponent<BoxCollider>();
        golfBallRb = GetComponent<Rigidbody>();

        // Define other object references
        arrow = GameObject.FindGameObjectWithTag("Arrow").GetComponent<Arrow>();
        powerMeter = GameObject.FindGameObjectWithTag("Power Meter").GetComponent<PowerMeter>();
        terrainCluster = GameObject.FindWithTag("Terrain");

        // Initiate collections
        currentlyColliding = new();

        // Set values
        golfBallStatus = BallStatus.AwaitingHit;
        powerMeterSpeed = 100.0f;
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
    }

    void OnCollisionEnter(Collision collision)
    {
        // If the collider is of Terrain type, add it's type to the list
        if (collision.transform.IsChildOf(terrainCluster.transform))
        {
            if (collision.gameObject.GetComponent<TerrainCollision>() == null)
            {
                Debug.LogError($"{collision.gameObject.name} has no TerrainType signed to it!");
                return;
            }
            currentlyColliding.Add(collision.gameObject.GetComponent<TerrainCollision>().terrainType);
            //Debug.Log($"Bouncing of {collision.gameObject.GetComponent<TerrainCollision>().terrainType}");
        }
    }

    void OnCollisionExit(Collision collision)
    {
        // If the collider is of Terrain type, remove it's type from the list
        if (collision.transform.IsChildOf(terrainCluster.transform))
        {
            if (collision.gameObject.GetComponent<TerrainCollision>() == null)
            {
                Debug.LogError("This GameObject in Terrain cluster has no TerrainType signed to it!");
                return;
            }
            currentlyColliding.Remove(collision.gameObject.GetComponent<TerrainCollision>().terrainType);
        }
    }

    void Deaccelerate()
    {
        // Apply friction
        if (currentlyColliding.Contains(TerrainType.Sand))
        {
            golfBallRb.drag = 6.0f;
        }
        else if (currentlyColliding.Contains(TerrainType.Grass))
        {
            golfBallRb.drag = 0.8f;
        }
        else
        {
            golfBallRb.drag = 0.1f;
        }

        // Check if nearly still
        if (golfBallRb.velocity.magnitude < 0.25f && !tryingToStop)
        {
            StartCoroutine("TryToStop");
        }
    }

    void Hit(float strength)
    {
        float forceX = strength * (1 - arrow.angleV / 90) * Mathf.Sin(arrow.angleH / 180 * Mathf.PI);
        float forceY = strength * (arrow.angleV / 90);
        float forceZ = strength * (1 - arrow.angleV / 90) * Mathf.Cos(arrow.angleH / 180 * Mathf.PI);

        golfBallRb.AddForce(forceX, forceY, forceZ, ForceMode.Impulse);
        golfBallRb.AddTorque(forceZ, 0, -forceX, ForceMode.Impulse);
    }

    IEnumerator TryToStop()
    {
        tryingToStop = true;

        yield return new WaitForSeconds(0.5f);

        if (golfBallRb.velocity.magnitude < 0.2f)
        {
            golfBallRb.velocity = new Vector3(0, 0, 0);
            golfBallRb.angularVelocity = new Vector3(0, 0, 0);
            golfBallStatus = BallStatus.AwaitingHit;
            EnableArrow();
        }
        else
        {
            golfBallStatus = BallStatus.Moving;
        }

        tryingToStop = false;
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
