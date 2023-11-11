using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPanelSpectrum : SpectrumBase
{
    [SerializeField] private float _addValue;
    public bool isRhythming;
    protected override void RhythmVisuallizing()
    {
        if (!isRhythming) return;

        _spectrumValue = UIManager.Instanace.bgm_SpectrumSizeValue;
        for (int i = 0; i < _visualGroup.Length; i++)
        {
            _visualGroup[i].sizeDelta =
                new Vector2(_startSizeDelta.x, _samples[i % 4 + 2] * (_spectrumValue + _addValue + (40 * i)));
        }
    }
}
