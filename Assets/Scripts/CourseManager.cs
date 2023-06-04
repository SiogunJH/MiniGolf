using UnityEngine;
using MessageBoxLib;
using UnityEngine.SceneManagement;

namespace CourseManagerLib
{
    public class CourseManager : MonoBehaviour
    {
        // References
        [SerializeField] private static MessageBox messageBox;

        // Variables
        private static CourseManager Instance { get; set; }
        public static int currentLevelID;
        public static int currentSectionID;

        void Awake()
        {
            // If exists
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            // One time functions
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        private void OnEnable() => SceneManager.sceneLoaded += OnSceneLoaded;
        private void OnDisable() => SceneManager.sceneLoaded -= OnSceneLoaded;

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

        public static void SendGameMessage(string message)
        {
            messageBox.gameObject.SetActive(true);
            messageBox.Send(message);
        }

        public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            // If main menu
            if (SceneManager.GetActiveScene().name == "Main Menu") return;

            // If course
            if (SceneManager.GetActiveScene().name == "Course")
            {
                Physics.gravity = new Vector3(0, -25, 0);

                //Prepare MessageBox
                messageBox = GameObject.FindWithTag("Message Box").gameObject.GetComponent<MessageBox>();
                messageBox.SetReferences();
                messageBox.gameObject.SetActive(false);
            }
        }
    }
}