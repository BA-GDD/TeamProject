using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LLineSpectrum : SpectrumBase
{
    [SerializeField] private RectTransform[] _rects = new RectTransform[7];
    private float[] _visualY = new float[8];

    private void Awake()
    {
        for(int i = 0; i < _visualY.Length; i++)
        {
            _visualY[i] = _visualGroup[i].localPosition.y;
        }
    }

    protected override void RhythmVisuallizing()
    {
        for (int i = 1; i < _visualGroup.Length; i++)
        {
            int random = Random.Range(-1, 1);
            if (random == 0) random = 1;
            

            _visualGroup[i].localPosition =  
            new Vector3(_visualGroup[i].localPosition.x, _visualY[i] + _samples[i % 4] * random * 100);
        }

        for(int i = 0; i < _rects.Length; i++)
        {
            Vector2 first = _visualGroup[i].localPosition;
            Vector2 second = _visualGroup[i + 1].localPosition;

            _rects[i].sizeDelta = new Vector2(10, 
                                  Mathf.Sqrt(Mathf.Pow(second.x - first.x, 2)
                                + Mathf.Pow(second.y - first.y, 2)));

            _rects[i].localPosition = (first + second) / 2;

            Vector2 dir = (second - first).normalized;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            _rects[i].localRotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        }
    }
}
