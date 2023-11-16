using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSpectrum : SpectrumBase
{
    [SerializeField] private RectTransform[] _rects = new RectTransform[7];
    private float[] _visualY = new float[8];
    [SerializeField] private float _addValue;

    private void Awake()
    {
        for (int i = 0; i < _visualY.Length; i++)
        {
            _visualY[i] = _visualGroup[i].localPosition.y;
        }
    }

    protected override void RhythmVisuallizing()
    {
        if (Time.frameCount % 3 != 0) return;

        _spectrumValue = UIManager.Instanace.bgm_SpectrumSizeValue;
        
        for (int i = 1; i < _visualGroup.Length-1; i++)
        {
            if (i % 2 == 0) continue;

            _visualGroup[i].localPosition =
            new Vector3(_visualGroup[i].localPosition.x, 
                        _visualY[i] + _samples[i % 5] * _spectrumValue * _addValue);
        }

        for (int i = 0; i < _rects.Length; i++)
        {
            Vector2 first = _visualGroup[i].localPosition;
            Vector2 second = _visualGroup[i + 1].localPosition;

            _rects[i].sizeDelta = new Vector2(4,
                                  Mathf.Sqrt(Mathf.Pow(second.x - first.x, 2)
                                + Mathf.Pow(second.y - first.y, 2)));

            _rects[i].localPosition = (first + second) / 2;

            Vector2 dir = (second - first).normalized;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            _rects[i].localRotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        }
    }
}
