using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualSpectrum : MonoBehaviour
{
    [SerializeField] private RectTransform[] barGroup = new RectTransform[8];
    private AudioSource audioSur;
    private float[] samples = new float[64];

    [SerializeField] private bool _isStart = false;

    private int _value;

    private void OnEnable()
    {
        for (int i = 0; i < barGroup.Length; i++)
        {
            barGroup[i].sizeDelta = new Vector2(30, 100);
        }

        audioSur = GetComponent<AudioSource>();
        audioSur.Play();
    }

    [ContextMenu("AudioPlay")]
    public void PlaySound()
    {
        audioSur.Play();
    }

    private void Update()
    {
        audioSur.GetSpectrumData(samples, 0, FFTWindow.Rectangular);
        _value = 100;
        for (int i = 0; i < barGroup.Length; i++)
        {
            barGroup[i].sizeDelta = new Vector2(30, samples[i] * (100 + (40 * i)));
        }
    }
}
