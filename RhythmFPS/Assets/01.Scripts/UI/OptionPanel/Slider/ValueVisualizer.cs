using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

[System.Serializable]
public class StandardValueAndSprite
{
    public float limit;
    public Sprite source;
}

public class ValueVisualizer : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Image _valueVisual;
    [SerializeField] private StandardValueAndSprite[] _standards;

    private void Start()
    {
        _standards = _standards.OrderByDescending(n => n.limit).ToArray();
    }

    public void ValueChangeEvent(float value)
    {
        for(int i = 0; i < _standards.Length; i++)
        {
            if(value * 100 <= _standards[i].limit)
            {
                _valueVisual.sprite = _standards[i].source;
            }
        }
    }
}
