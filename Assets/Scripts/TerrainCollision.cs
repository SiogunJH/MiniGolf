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
        if (terrainCollider != null) terrainCollider.material.bounciness = 0.5f;
        else Debug.LogError("GameObject has no collider!");
    }
}