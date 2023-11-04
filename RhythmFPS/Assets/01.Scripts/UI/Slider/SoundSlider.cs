using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSlider : MonoBehaviour
{
    [SerializeField] private SoundType _soundType;
    [SerializeField] private AudioSource _audioSource;

    private void Awake()
    {
        // 오디오 소스 찾기
    }

    public void SoundChange(float value)
    {
        _audioSource.volume = value;
        UIManager.Instance.SetSpectrumValue(_soundType, value);
    }
}
