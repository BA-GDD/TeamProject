using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSpectrum : SpectrumBase
{
    protected override void RhythmVisuallizing()
    {
        for (int i = 0; i < _visualGroup.Length; i++)
        {
            _visualGroup[i].sizeDelta = new Vector2(_startSizeDelta.x, _samples[i] * (100 + (40 * i)));
        }
    }
}
