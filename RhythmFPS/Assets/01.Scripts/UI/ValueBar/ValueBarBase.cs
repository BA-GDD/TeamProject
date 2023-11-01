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
    public float maxValue // �ܺο��� ���� �ʼ�
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
    [SerializeField] private float _lerpTime; // ���� �ӵ�
    private bool isLerping; // ���� üũ

    private void Start()
    {
        if (_hpValueText != null)
            _hpValueText.text = $"{_maxValue} / {_curenValue}";
    }

    public virtual void GetDamage(float damage) // Hp �Ű������� ���� damage ��ŭ ���
    {
        _curenValue -= Mathf.Clamp(_curenValue - damage, 0, _maxValue);
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
