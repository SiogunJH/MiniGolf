using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainCollision : MonoBehaviour
{
    private Collider terrainCollider;
    public TerrainType terrainType;

    void Start()
    {
        // Set variables
        terrainCollider = gameObject.GetComponent<Collider>();

        // Set bounciness
        if (terrainCollider != null)
        {
            terrainCollider.material.bounciness = GetBounciness();
        }
        else Debug.LogError($"{gameObject.name} has no collider!");
    }

    float GetBounciness()
    {
        if (terrainType == TerrainType.Wood) return 0.7f;
        if (terrainType == TerrainType.Grass) return 0.5f;
        if (terrainType == TerrainType.Sand) return 0.025f;


        Debug.LogError($"Terrain Type of {terrainType} has no bounciness signed to it!");
        return 0;
    }
}