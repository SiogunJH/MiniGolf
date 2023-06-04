using System.Collections.Generic;
using UnityEngine;
using TerrainLib;

public partial class GolfBall : MonoBehaviour
{
    // Parent of all Terrain elements
    [HideInInspector] public GameObject terrainCluster;

    // List of TerrainTypes currently colliding with GolfBall
    [SerializeField] private List<TerrainType> currentlyColliding;

    //On entering collision
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
            Deaccelerate();
        }
    }

    //On exiting collision
    void OnCollisionExit(Collision collision)
    {
        // If the collider is of Terrain type, remove it's type from the list
        if (collision.transform.IsChildOf(terrainCluster.transform))
        {
            if (collision.gameObject.GetComponent<TerrainCollision>() == null)
            {
                Debug.LogError($"{collision.gameObject.name} has no TerrainType signed to it!");
                return;
            }
            currentlyColliding.Remove(collision.gameObject.GetComponent<TerrainCollision>().terrainType);
            Deaccelerate();
        }
    }

    // Pick out the strongest collider and apply its friction
    void Deaccelerate()
    {
        // Apply friction
        if (currentlyColliding.Contains(TerrainType.End))
        {
            Rb.drag = 20.0f;
        }
        else if (currentlyColliding.Contains(TerrainType.Sand))
        {
            Rb.drag = 6.0f;
        }
        else if (currentlyColliding.Contains(TerrainType.Grass))
        {
            Rb.drag = 0.8f;
        }
        else if (currentlyColliding.Contains(TerrainType.OutOfBound))
        {
            Rb.drag = 0.8f;
        }
        else if (currentlyColliding.Contains(TerrainType.Wood))
        {
            Rb.drag = 0.25f;
        }
        else if (currentlyColliding.Contains(TerrainType.Plastic))
        {
            Rb.drag = 0.2f;
        }
        else
        {
            Rb.drag = 0.1f;
        }
    }
}
