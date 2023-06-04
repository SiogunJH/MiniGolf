using UnityEngine;
using MessageBoxLib;
using UnityEngine.SceneManagement;

namespace CourseManagerLib
{
    public partial class CourseManager : MonoBehaviour
    {
        public static int currentLevelID;
        public static int currentSectionID;
        public static Vector3 GetStartingPoint(int levelID)
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
    }
}