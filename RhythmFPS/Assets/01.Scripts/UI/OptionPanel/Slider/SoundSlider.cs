using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundSlider : MonoBehaviour
{
    [SerializeField] private SoundType _soundType;
    [SerializeField] private AudioMixer _audioMixer;
    private Slider _thiSlider;

    private void Awake()
    {
        _thiSlider = GetComponent<Slider>();
    }

    private void Start()
    {
        if (_thiSlider != null)
        {

            float value = _soundType == SoundType.BGM ? SaveManager.Instance.data.BGMValue : SaveManager.Instance.data.SFXValue;
            _audioMixer.SetFloat(_soundType.ToString(), Mathf.Lerp(-60, 20, value));

            _thiSlider.value = value;
            Debug.Log(_thiSlider);
        }

    }

    public void SoundChange(float value)
    {
        //_thiSlider.value = Mathf.Lerp(-100,100,value);
        _audioMixer.SetFloat(_soundType.ToString(), Mathf.Lerp(-60, 20, value));
        UIManager.Instanace.SetSpectrumValue(_soundType, value);
        if (_soundType == SoundType.BGM)
        {
            SaveManager.Instance.data.BGMValue = value;
        }
        else
        {
            SaveManager.Instance.data.SFXValue = value;
        }
    }
}
