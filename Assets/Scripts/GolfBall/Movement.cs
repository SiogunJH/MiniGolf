using System.Collections;
using UnityEngine;
using TerrainLib;
using CourseManagerLib;
using UtilityLib;

public partial class GolfBall : MonoBehaviour
{
    private bool tryingToStop;
    public Vector3 lastPos;

    /// <summary>
    /// Add velocity to the Golf Ball in the direction the arrow is pointing
    /// </summary>
    /// <param name="strength">Strength of a hit</param>
    void Hit(float strength)
    {
        float forceX = strength * (1 - arrow.rot.x / 90) * Mathf.Sin(arrow.rot.y.ToRadians());
        float forceY = strength * (0 + arrow.rot.x / 90);
        float forceZ = strength * (1 - arrow.rot.x / 90) * Mathf.Cos(arrow.rot.y.ToRadians());

        Rb.AddForce(forceX, forceY, forceZ, ForceMode.Impulse);
        Rb.AddTorque(forceZ, 0, -forceX, ForceMode.Impulse);
    }

    /// <summary>
    /// If the ball has velocity magnitude below 0.2 for over 0.5 seconds, stop and run PrepareForHit. Else, do nothing.
    /// </summary>
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

    /// <summary>
    /// Move the Golf Ball to specified position, without affecting its velocity nor its angular velocity.
    /// </summary>
    /// <param name="pos">Targeted position in which the Golf Ball will appear</param>
    public void GoTo(Vector3 pos)
    {
        transform.position = pos;
    }

    /// <summary>
    /// Set the Golf Ball velicty to 0, and thus stop its movement.
    /// </summary>
    /// <returns>Golf Ball velocity right before stopping</returns>
    public Vector3 StopMovement()
    {
        var velocity = Rb.velocity;
        Rb.velocity = Vector3.zero;
        return velocity;
    }

    /// <summary>
    /// Set the Golf Ball angular velicty to 0, and thus stop its rotational movement.
    /// </summary>
    /// <returns>Golf Ball angular velocity right before stopping</returns>
    public Vector3 StopRotation()
    {
        var angularVelocity = Rb.angularVelocity;
        Rb.angularVelocity = Vector3.zero;
        return angularVelocity;
    }

    /// <summary>
    /// Enable arrow and allow player to hit
    /// </summary>
    /// <remarks>
    /// Behave different, depending on what terrain the Golf Ball in collision with:
    /// <list type="bullet">
    ///     <item>OutOfBound Terrain: Send the OutOfBounds message, go back and allow for hit</item>
    ///     <item>End Terrain: Go to the next level</item>
    ///     <item>Default: Update lastPos and allow for hit</item>
    /// </list>
    /// </remarks>
    void PrepareForHit()
    {
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
            // Complete level
            CourseManager.OpenLevelCompletionMenu();
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
