using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PowerMeterLib
{
    public class PowerMeter : MonoBehaviour
    {
        // References
        public Slider slider;

        // Properties
        public float sliderValue
        {
            get => slider.value;
            set => slider.value = value;
        }

        // Start
        void Start()
        {
            slider = GetComponent<Slider>();
        }

        // Modify slider value
        public void ChangeValue(float delta) => slider.value += delta;
    }
}
