using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GolfBall : MonoBehaviour
{
    // Pick out the strongest collider and apply its friction
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
}
