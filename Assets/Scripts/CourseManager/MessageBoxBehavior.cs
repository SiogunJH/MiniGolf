using UnityEngine;
using MessageBoxLib;
using UnityEngine.SceneManagement;

namespace CourseManagerLib
{
    public partial class CourseManager : MonoBehaviour
    {
        private static MessageBox messageBox;
        public static void SendGameMessage(string message)
        {
            messageBox.gameObject.SetActive(true);
            messageBox.Send(message);
        }
    }
}