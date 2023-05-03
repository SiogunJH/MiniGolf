using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GolfBall : MonoBehaviour
{
    [SerializeField] private List<TerrainType> currentlyColliding;
    private const float bounciness = 0.75f;
    public Collider golfBallCol;
    public Rigidbody golfBallRb;

    void Start()
    {
        // Define variables
        golfBallCol = GetComponent<Collider>();
        golfBallRb = GetComponent<Rigidbody>();
        currentlyColliding = new();

        // Set bounciness
        if (golfBallCol != null) golfBallCol.material.bounciness = bounciness;
        else Debug.LogError("GameObject has no collider!");
    }

    void Update()
    {
        DeaccelerateGolfBall();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Terrain"))
            currentlyColliding.Add(collision.gameObject.GetComponent<TerrainCollision>().terrainType);
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Terrain"))
        {
            if (currentlyColliding.Contains(collision.gameObject.GetComponent<TerrainCollision>().terrainType))
                currentlyColliding.Remove(collision.gameObject.GetComponent<TerrainCollision>().terrainType);
            else
                Debug.LogError("Exiting collision from terrain that was never entered!");
        }
    }

    void DeaccelerateGolfBall()
    {
        if (currentlyColliding.Contains(TerrainType.Grass))
        {
            if (golfBallRb.velocity.magnitude > 5.0f)
                golfBallRb.AddForce(-golfBallRb.velocity * 0.03f);
            else if (golfBallRb.velocity.magnitude > 1.0f)
                golfBallRb.AddForce(-golfBallRb.velocity * 0.1f);
            else if (golfBallRb.velocity.magnitude > 0.1f)
                golfBallRb.AddForce(-golfBallRb.velocity * 0.5f);
        }

        //TODO: 
        //      Check if nearly still for over 1 second
        //      then stop completely and allow player to shoot
    }
}
