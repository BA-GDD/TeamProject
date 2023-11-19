using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SenseSlider : MonoBehaviour
{
    Slider slider;
    private void Awake()
    {
        slider = GetComponent<Slider>();
    }
    private void Start()
    {
        slider.value = SaveManager.Instance.data.senseValue;
    }
    public void SetSensitivity(float value)
    {
        SaveManager.Instance.data.senseValue = value;
    }
}
