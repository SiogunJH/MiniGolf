using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerMeter : MonoBehaviour
{
    public Slider slider;
    public float sliderValue
    {
        get => slider.value;
        set => slider.value = value;
    }

    void Start()
    {
        slider = transform.Find("Power Slider").GetComponent<Slider>();
    }

    public void ChangeValue(float delta) => slider.value += delta;
}
