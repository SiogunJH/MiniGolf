using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CourseManager : MonoBehaviour
{
    public static CourseManager Instance { get; private set; }
    public int currentLevelID;

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

    public Vector3 GetStartingPoint(int levelID)
    {
        switch (levelID)
        {
            case 1:
                return GameObject.FindGameObjectWithTag("Terrain").transform.Find("Level 1-1").transform.Find("Starting Point").transform.position;
            case 2:
                return GameObject.FindGameObjectWithTag("Terrain").transform.Find("Level 1-2").transform.Find("Starting Point").transform.position;
        }

        Debug.LogError($"Level {levelID} was not found!");
        return new Vector3();
    }

    public void SendGameMessage(string message)
    {
        GameObject.FindGameObjectWithTag("Message Box").GetComponent<MessageBox>().Send(message);
    }

    public void FirstTimeSetup()
    {
        Physics.gravity = new Vector3(0, -25, 0);
    }
}
