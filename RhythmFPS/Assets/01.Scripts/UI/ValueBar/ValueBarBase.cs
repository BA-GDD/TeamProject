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
    [SerializeField] private float _lerpTime; // ���� �ӵ�
    private bool isLerping; // ���� üũ

    private void Start()
    {
        _curenValue = _maxValue;
        if (_hpValueText != null)
            _hpValueText.text = $"{_maxValue} / {_curenValue}";
    }

    public virtual void DiminishValue(float value) // value ��ŭ ���
    {
        _curenValue = Mathf.Clamp(_curenValue - value, 0, _maxValue);
        isLerping = true;

        if(_hpValueText != null)
            _hpValueText.text = $"{_maxValue} / {_curenValue}";
        
    }

    private void Update()
    {
        if(isLerping) // ���� �߿��� Update ȣ��
        {
            _hpBar.value = Mathf.Lerp(_hpBar.value, _curenValue / _maxValue, Time.deltaTime * _lerpTime);

            if (_hpBar.value == _curenValue / _maxValue) 
                isLerping = false;
        }
    }
}
