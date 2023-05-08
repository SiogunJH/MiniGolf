using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GolfBall : MonoBehaviour
{
    // Parent of all Terrain elements
    public GameObject terrainCluster;

    // List of TerrainTypes currently colliding with GolfBall
    private List<TerrainType> currentlyColliding;

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
}
