using UnityEngine;
using ArrowLib;

public partial class GolfBall : MonoBehaviour
{
    // Arrow reference
    private Arrow arrow;

    // Disable
    void DisableArrow()
    {
        arrow.gameObject.SetActive(false);
    }

    // Enable
    void EnableArrow()
    {
        arrow.gameObject.SetActive(true);
    }
}
