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
    public void EnableArrow()
    {
        arrow.gameObject.SetActive(true);
    }

    public void ResetArrow()
    {
        arrow.rot = new();
    }
}
