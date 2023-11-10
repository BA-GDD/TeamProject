using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSpectrum : SpectrumBase
{
    [SerializeField] private float _addValue;
    protected override void RhythmVisuallizing()
    {
        _spectrumValue = UIManager.Instnace.bgm_SpectrumSizeValue;
        for (int i = 0; i < _visualGroup.Length; i++)
        {
            _visualGroup[i].sizeDelta = 
                new Vector2(_startSizeDelta.x, _samples[i % 3 + 2] * (_spectrumValue + _addValue + (40 * i)));
        }
    }
}
