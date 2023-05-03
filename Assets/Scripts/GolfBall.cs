using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GolfBall : MonoBehaviour
{
    [SerializeField] private List<TerrainType> currentlyColliding;
    private const float bounciness = 0.75f;
    public Collider golfBallCol;
    public Rigidbody golfBallRb;
    public Arrow arrow;

    void Start()
    {
        // Define variables
        golfBallCol = GetComponent<Collider>();
        golfBallRb = GetComponent<Rigidbody>();
        currentlyColliding = new();
        arrow = GameObject.FindGameObjectWithTag("Arrow").GetComponent<Arrow>();

        // Set bounciness
        if (golfBallCol != null) golfBallCol.material.bounciness = bounciness;
        else Debug.LogError("GameObject has no collider!");
    }

    void Update()
    {
        DeaccelerateGolfBall();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Hit(40);
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
        {
            if (currentlyColliding.Contains(collision.gameObject.GetComponent<TerrainCollision>().terrainType))
                currentlyColliding.Remove(collision.gameObject.GetComponent<TerrainCollision>().terrainType);
            else //This error should never be sent
                Debug.LogError("Exiting collision from terrain that was never entered!");
        }
    }

    void DeaccelerateGolfBall()
    {
        /*
                Pick out the strongest terrain friction
                from list of terrain list and apply it
        */
        if (currentlyColliding.Contains(TerrainType.Grass))
        {
            if (golfBallRb.velocity.magnitude > 5.0f)
                golfBallRb.AddForce(-golfBallRb.velocity * 0.025f);
            else if (golfBallRb.velocity.magnitude > 1.0f)
                golfBallRb.AddForce(-golfBallRb.velocity * 0.1f);
            else if (golfBallRb.velocity.magnitude > 0.1f)
                golfBallRb.AddForce(-golfBallRb.velocity * 0.25f);
        }

        /*
            TODO: 
                Check if nearly still for over 1 second
                then stop completely and allow player to shoot
        */
    }

    void Hit(float strength)
    {
        float forceX = strength * Mathf.Sin(arrow.angleH / 180 * Mathf.PI);
        float forceY = strength * (arrow.angleV / 80);
        float forceZ = strength * Mathf.Cos(arrow.angleH / 180 * Mathf.PI);

        golfBallRb.velocity = new Vector3(forceX, forceY, forceZ);

        golfBallRb.AddTorque(Random.Range(1, 30), Random.Range(1, 30), Random.Range(1, 30));
    }
}
