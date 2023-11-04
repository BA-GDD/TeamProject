using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpectrumBase : MonoBehaviour
{
    [SerializeField] protected RectTransform[] _visualGroup = new RectTransform[8];
    private AudioSource _audioSur;
    protected float[] _samples = new float[64];
    [SerializeField] protected Vector2 _startSizeDelta;

    private void OnEnable()
    {
        for (int i = 0; i < _visualGroup.Length; i++)
        {
            _visualGroup[i].sizeDelta = _startSizeDelta;
        }

        _audioSur = GameObject.Find("AudioAmple").GetComponent<AudioSource>();
    }

    private void Update()
    {
        _audioSur.GetSpectrumData(_samples, 0, FFTWindow.Rectangular);
        RhythmVisuallizing();
    }

    protected abstract void RhythmVisuallizing();
}
