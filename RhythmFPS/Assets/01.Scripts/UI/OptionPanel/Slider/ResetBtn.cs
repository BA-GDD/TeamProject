using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetBtn : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private float _resetValue;

    public void ResetValue()
    {
        _slider.value = _resetValue;
    }
}
