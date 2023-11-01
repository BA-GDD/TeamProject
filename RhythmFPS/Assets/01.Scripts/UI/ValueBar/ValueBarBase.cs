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
    private float _maxValue;
    public float maxValue // 외부에서 설정 필수
    {
        get
        {
            return _maxValue;
        }
        set
        {
            _maxValue = _curenValue = value;
        }
    }
    [SerializeField] private float _lerpTime; // 러핑 속도
    private bool isLerping; // 러핑 체크

    private void Start()
    {
        if (_hpValueText != null)
            _hpValueText.text = $"{_maxValue} / {_curenValue}";
    }

    public virtual void GetDamage(float damage) // Hp 매개변수로 받은 damage 만큼 까기
    {
        _curenValue -= Mathf.Clamp(_curenValue - damage, 0, _maxValue);
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
