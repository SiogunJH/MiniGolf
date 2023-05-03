using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CourseManager : MonoBehaviour
{
    public static CourseManager Instance { get; private set; }

    void Awake()
    {
        // If exists
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        FirstTimeSetup();
        Instance = this;
    }

    public void FirstTimeSetup()
    {
        Physics.gravity = new Vector3(0, -25, 0);
    }
}
