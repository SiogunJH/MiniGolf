using UnityEngine;
using MessageBoxLib;
using UnityEngine.SceneManagement;

namespace CourseManagerLib
{
    public partial class CourseManager : MonoBehaviour
    {
        // Variables
        private static CourseManager Instance { get; set; }

        // On Awake
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
        public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            // If main menu
            if (SceneManager.GetActiveScene().name == "Main Menu")
            {
                return;
            }

            // If course
            if (SceneManager.GetActiveScene().name == "Course")
            {
                // Set Physics
                Physics.gravity = new Vector3(0, -25, 0);

                // Prepare MessageBox
                messageBox = GameObject.FindWithTag("Message Box").gameObject.GetComponent<MessageBox>();
                messageBox.SetReferences();
                messageBox.gameObject.SetActive(false);

                // Prepare GolfBall
                golfBall = GameObject.FindWithTag("Golf Ball").gameObject.GetComponent<GolfBall>();
            }
        }
    }
}