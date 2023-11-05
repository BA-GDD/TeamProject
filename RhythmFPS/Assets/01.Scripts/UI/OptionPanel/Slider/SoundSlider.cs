using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSlider : MonoBehaviour
{
    [SerializeField] private SoundType _soundType;
    private AudioSource _audioSource;
    private Slider _thiSlider;

    private void Awake()
    {
        _thiSlider = GetComponent<Slider>();
        // 오디오 소스 찾기
        if(_soundType == SoundType.bgm)
            _audioSource = GameObject.Find("AudioAmple").GetComponent<AudioSource>(); // 
        else
            _audioSource = GameObject.Find("AudioAmple").GetComponent<AudioSource>();
    }

    private void Start()
    {
        if(_thiSlider != null)
        {
            _thiSlider.value = _audioSource.volume;
            Debug.Log(_thiSlider);
        }
            
    }

    public void SoundChange(float value)
    {
        _audioSource.volume = value;
        UIManager.Instance.SetSpectrumValue(_soundType, value);
    }
}
