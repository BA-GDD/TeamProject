using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteBox : MonoBehaviour
{
    [Header("셋팅")]
    [SerializeField] private Toggle _muteBox;
    [SerializeField] private Slider _slider;

    [Header("참조")]
    [SerializeField] private SoundSlider _soundSlider;
    [SerializeField] private ValueVisualizer _visualizer;

    private void Start()
    {
        SetMute(_muteBox.isOn);
    }

    public void SetMute(bool isMute)
    {
        if(isMute)
        {
            _soundSlider.SoundChange(0);
            _visualizer.ValueChangeEvent(0);

            _slider.onValueChanged.RemoveAllListeners();
        }
        else
        {
            _slider.onValueChanged.AddListener(_soundSlider.SoundChange);
            _slider.onValueChanged.AddListener(_visualizer.ValueChangeEvent);
            _slider.onValueChanged?.Invoke(_slider.value);
            Debug.Log(_slider.value);
        }
    }
}
