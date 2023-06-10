using System.Collections;
using UnityEngine;
using TerrainLib;
using CourseManagerLib;
using UtilityLib;

public partial class GolfBall : MonoBehaviour
{
    private bool tryingToStop;
    private Vector3 lastPos;

    void Hit(float strength)
    {
        float forceX = strength * (1 - arrow.rot.x / 90) * Mathf.Sin(arrow.rot.y.ToRadians());
        float forceY = strength * (0 + arrow.rot.x / 90);
        float forceZ = strength * (1 - arrow.rot.x / 90) * Mathf.Cos(arrow.rot.y.ToRadians());

        Rb.AddForce(forceX, forceY, forceZ, ForceMode.Impulse);
        Rb.AddTorque(forceZ, 0, -forceX, ForceMode.Impulse);
    }

    IEnumerator TryToStop()
    {
        tryingToStop = true;

        yield return new WaitForSeconds(0.5f);

        if (Rb.velocity.magnitude < 0.2f && Status != BallStatus.Disabled)
        {
            StopMovement();
            StopRotation();
            PrepareForHit();
        }
        else if (Status != BallStatus.Disabled)
        {
            Status = BallStatus.Moving;
        }

        tryingToStop = false;
    }

    void GoTo(Vector3 pos)
    {
        Rb.velocity = new Vector3(0, 0, 0);
        Rb.angularVelocity = new Vector3(0, 0, 0);
        transform.position = pos;
    }

    public Vector3 StopMovement()
    {
        var velocity = Rb.velocity;
        Rb.velocity = Vector3.zero;
        return velocity;
    }

    public Vector3 StopRotation()
    {
        var angularVelocity = Rb.angularVelocity;
        Rb.angularVelocity = Vector3.zero;
        return angularVelocity;
    }

    void PrepareForHit()
    {
        //Stop and prepare for hit
        Status = BallStatus.AwaitingHit;
        EnableArrow();

        if (currentlyColliding.Contains(TerrainType.OutOfBound)) //When out of bounds
        {
            //Send OutOfBounds message
            CourseManager.SendGameMessage(Messages.OutOfBounds.All[Random.Range(0, Messages.OutOfBounds.All.Count)]);

            //Go one position back
            GoTo(lastPos);

            //Allow hit
            Status = BallStatus.AwaitingHit;
            EnableArrow();
        }
        else if (currentlyColliding.Contains(TerrainType.End)) //When in the hole
        {
            //Go to the next level
            CourseManager.currentLevelID++;
            GoTo(CourseManager.GetStartingPoint(CourseManager.currentLevelID));

            //Set last position as current
            lastPos = transform.position;
        }
        else //When still on the course
        {
            //Update last position
            lastPos = transform.position;

            //Allow hit
            Status = BallStatus.AwaitingHit;
            EnableArrow();
        }
    }
}
