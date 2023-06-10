using UnityEngine;
using ArrowLib;
using PowerMeterLib;
using CourseManagerLib;

public partial class GolfBall : MonoBehaviour
{
    // Golf Ball refs and vars
    public BoxCollider Col;
    public Rigidbody Rb;
    public BallStatus Status;

    // Power Meter refs and vars
    private PowerMeter powerMeter;
    private float powerMeterSpeed;

    void Start()
    {
        // Define GolfBall refs and vars
        Col = GetComponent<BoxCollider>();
        Rb = GetComponent<Rigidbody>();
        Status = BallStatus.AwaitingHit;

        // Define Arrow ref and vars
        arrow = GameObject.FindGameObjectWithTag("Arrow").GetComponent<Arrow>();

        // Define Power Meter refs and vars
        powerMeter = GameObject.FindGameObjectWithTag("Power Meter").GetComponent<PowerMeter>();
        powerMeterSpeed = powerMeter.slider.maxValue * 1.5f;

        // Define other
        terrainCluster = GameObject.FindWithTag("Terrain");
        currentlyColliding = new();
        tryingToStop = false;
        lastPos = CourseManager.GetStartingPoint(CourseManager.currentLevelID);

        // Go to position defined by CourseManager
        transform.position = CourseManager.GetStartingPoint(CourseManager.currentLevelID);
        Status = BallStatus.AwaitingHit;
    }

    void Update()
    {
        // Check if nearly still and if so, try to stop
        if (Status == BallStatus.Moving && !tryingToStop && Rb.velocity.magnitude < 0.25f)
        {
            StartCoroutine("TryToStop");
        }

        // Disable Hit Mechanic if the GolfBall started Moving from being AwaitingHit
        if (Status == BallStatus.AwaitingHit && Rb.velocity.magnitude > 0.1f)
        {
            powerMeter.sliderValue = 0;
            Status = BallStatus.Moving;
            DisableArrow();
        }

        // Hit Mechanic
        if (Status == BallStatus.AwaitingHit && Input.GetKey(KeyBindsManager.KeyBinds[KeyAction.LoadPowerMeter]))
        {
            powerMeter.sliderValue = powerMeter.sliderValue + Time.deltaTime * powerMeterSpeed;
        }
        else if (Status == BallStatus.AwaitingHit && Input.GetKeyUp(KeyBindsManager.KeyBinds[KeyAction.LoadPowerMeter]))
        {
            Hit(powerMeter.sliderValue);

            powerMeter.sliderValue = 0;
            Status = BallStatus.Moving;
            DisableArrow();
        }
    }
}
