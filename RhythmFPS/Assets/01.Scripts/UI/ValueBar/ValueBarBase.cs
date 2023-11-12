using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ValueBarBase : MonoBehaviour
{
    [SerializeField] private Slider _hpBar;
    [SerializeField] private TextMeshProUGUI _hpValueText;

    private float _curenValue;
    [SerializeField] private float _maxValue;
    [SerializeField] private float _lerpTime; // 러핑 속도
    private bool isLerping; // 러핑 체크

    private void Start()
    {
        _curenValue = _maxValue;
        if (_hpValueText != null)
            _hpValueText.text = $"{_maxValue} / {_curenValue}";
    }

    public virtual void DiminishValue(float value) // value 만큼 까기
    {
        _curenValue = Mathf.Clamp(_curenValue - value, 0, _maxValue);
        isLerping = true;

        if(_hpValueText != null)
            _hpValueText.text = $"{_maxValue} / {_curenValue}";
        
    }

    private void Update()
    {
        if(isLerping) // 러핑 중에만 Update 호출
        {
            _hpBar.value = Mathf.Lerp(_hpBar.value, _curenValue / _maxValue, Time.deltaTime * _lerpTime);

            if (_hpBar.value == _curenValue / _maxValue) 
                isLerping = false;
        }
    }
}
