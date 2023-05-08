using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GolfBall : MonoBehaviour
{
    // Arrow reference
    public Arrow arrow;

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
