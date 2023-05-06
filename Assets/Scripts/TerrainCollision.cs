using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainCollision : MonoBehaviour
{
    private BoxCollider terrainCollider;
    public TerrainType terrainType;

    void Start()
    {
        // Set variables
        terrainCollider = gameObject.GetComponent<BoxCollider>();

        // Set bounciness
        if (terrainCollider != null)
        {
            terrainCollider.material.bounciness = GetBounciness();
        }
        else Debug.LogError("GameObject has no collider!");
    }

    float GetBounciness()
    {
        if (terrainType == TerrainType.Grass) return 0.675f;
        if (terrainType == TerrainType.Sand) return 0.0f;


        Debug.LogError($"Terrain Type of {terrainType} has no bounciness signed to it!");
        return 0;
    }
}