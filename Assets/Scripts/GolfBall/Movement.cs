using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GolfBall : MonoBehaviour
{
    private bool tryingToStop;
    private Vector3 lastPos;

    void Hit(float strength)
    {
        float forceX = strength * (1 - arrow.rotX / 90) * Mathf.Sin(arrow.rotY.ToRadians());
        float forceY = strength * (0 + arrow.rotX / 90);
        float forceZ = strength * (1 - arrow.rotX / 90) * Mathf.Cos(arrow.rotY.ToRadians());

        golfBallRb.AddForce(forceX, forceY, forceZ, ForceMode.Impulse);
        golfBallRb.AddTorque(forceZ, 0, -forceX, ForceMode.Impulse);
    }

    IEnumerator TryToStop()
    {
        tryingToStop = true;

        yield return new WaitForSeconds(0.5f);

        if (golfBallRb.velocity.magnitude < 0.2f) Stop();
        else golfBallStatus = BallStatus.Moving;

        tryingToStop = false;
    }

    void GoTo(Vector3 pos)
    {
        golfBallRb.velocity = new Vector3(0, 0, 0);
        golfBallRb.angularVelocity = new Vector3(0, 0, 0);
        transform.position = pos;
    }

    void Stop()
    {
        //Stop and prepare for hit
        golfBallRb.velocity = new Vector3(0, 0, 0);
        golfBallRb.angularVelocity = new Vector3(0, 0, 0);
        golfBallStatus = BallStatus.AwaitingHit;
        EnableArrow();

        if (currentlyColliding.Contains(TerrainType.OutOfBound)) //When out of bounds
        {
            //Send OutOfBounds message
            CourseManager.Instance.SendGameMessage(Messages.Sets.outOfBounds[Random.Range(0, Messages.Sets.outOfBounds.Count)]);

            //Go one position back
            GoTo(lastPos);
        }
        else if (currentlyColliding.Contains(TerrainType.Hole)) //When in the hole
        {
            //Go to the next level
            CourseManager.Instance.currentLevelID++;
            GoTo(CourseManager.Instance.GetStartingPoint(CourseManager.Instance.currentLevelID));
        }
        else //When still on the course
        {
            //Update last position
            lastPos = transform.position;

            // Update statistics
        }
    }
}
