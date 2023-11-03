using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualSpectrum : MonoBehaviour
{
    [SerializeField] protected RectTransform[] _barGroup = new RectTransform[8];
    private AudioSource _audioSur;
    private float[] _samples = new float[64];
    [SerializeField] private Vector2 _startSizeDelta;
    [SerializeField] private bool _isStart = false;

    private void OnEnable()
    {
        for (int i = 0; i < _barGroup.Length; i++)
        {
            _barGroup[i].sizeDelta = _startSizeDelta;
        }

        _audioSur = GameObject.Find("AudioAmple").GetComponent<AudioSource>();
    }

    private void Update()
    {
        //if (!_isStart) return;

        _audioSur.GetSpectrumData(_samples, 0, FFTWindow.Rectangular);
        for (int i = 0; i < _barGroup.Length; i++)
        {
            _barGroup[i].sizeDelta = new Vector2(30, _samples[i] * (100 + (40 * i)));
        }
    }
}
