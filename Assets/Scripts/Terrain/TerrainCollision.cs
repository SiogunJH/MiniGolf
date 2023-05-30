using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TerrainLib
{
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
            // Search bouncinessDict for bounciness value
            if (TerrainBounciness.Dict.ContainsKey(terrainType))
                return TerrainBounciness.Dict[terrainType];

            // If value is not found, log error
            Debug.LogError($"Terrain Type of {terrainType} has no bounciness signed to it!");
            return 0;
        }
    }
}