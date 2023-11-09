using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;

public class ComboCountUI : MonoBehaviour
{
    private int combo;
    [Header("ÄÞº¸ ÅØ½ºÆ®")]
    [SerializeField] private TextMeshProUGUI[] _comboText;

    [SerializeField] private RectTransform[] _comboCountTextTrm;
    [SerializeField] private RectTransform[] _comboTextTrm;

    [SerializeField] private float _easingTime = 0.2f;
    private bool isEasing;

    [Header("ÄÞº¸ ¸®¹ÌÆ®")]
    private bool _isBurnning;
    private bool _isSecBurnning;
    [SerializeField] private int _burnningLimit;
    [SerializeField] private int _secondBurnningLimit;
    [SerializeField] private UnityEvent _comboBurnningEvent;
    [SerializeField] private UnityEvent _secondComboBurnningEvent;
    [SerializeField] private UnityEvent _comboResetEvent;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            ComboPlus();
        if (Input.GetKeyDown(KeyCode.K))
            ResetCombo();
    }


    [ContextMenu("ÄÞº¸+1")]
    public void ComboPlus()
    {
        combo++;
        if(combo >= _burnningLimit && !_isBurnning)
        {
            _comboBurnningEvent?.Invoke();
            _isBurnning = true;
        }
        if(combo >= _secondBurnningLimit && !_isSecBurnning)
        {
            _secondComboBurnningEvent?.Invoke();
            _isSecBurnning = true;
        }
        ApplyText();
    }

    [ContextMenu("¸®¼Â")]
    public void ResetCombo()
    {
        combo = 0;

        if(_isBurnning)
        {
            _comboResetEvent?.Invoke();
            _isBurnning = false;
            _isSecBurnning = false;
        }
        ApplyText();
    }

    private void ApplyText()
    {
        _comboText[0].text = combo.ToString();
        _comboText[1].text = combo.ToString();

        _comboCountTextTrm[0].localScale = new Vector3(1.3f, 1.3f, 0);
        _comboCountTextTrm[1].localScale = new Vector3(1.3f, 1.3f, 0);

        if (isEasing)
        {
            _comboCountTextTrm[0].localScale = Vector3.one;
            _comboCountTextTrm[1].localScale = Vector3.one;
        }

        isEasing = true;
        Sequence seq = DOTween.Sequence();
        seq.Append(_comboCountTextTrm[0].DOScale(Vector3.one, _easingTime));
        seq.Join(_comboCountTextTrm[1].DOScale(Vector3.one, _easingTime));
        for (int i = 0; i < 2; i++)
        {
            seq.Join(_comboCountTextTrm[i].DOShakePosition(0.6f, 5f));
            seq.Join(_comboTextTrm[i].DOShakePosition(0.6f, 5f));
        }
        seq.AppendCallback(() =>
        {
            isEasing = false;
        });
    }
}
