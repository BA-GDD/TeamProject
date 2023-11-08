using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class ComboFire : MonoBehaviour
{
    [SerializeField] private Color[] _gradationColor;
    [SerializeField] private float _easingTime;
    private Sequence fireSequence;
    private Image _fireImg;

    private bool _isBurnning;
    public bool IsBurnning
    {
        get
        {
            return _isBurnning;
        }
        set
        {
            _isBurnning = value;
            if(_isBurnning)
            {
                StartCoroutine(BurnningLoop());
            }
            else
            {
                KillBurnningLoop();
            }
        }
    }

    private void Awake()
    {
        _fireImg = GetComponent<Image>();
        _fireImg.fillAmount = 0;
    }

    private void KillBurnningLoop()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(_fireImg.DOFade(0, _easingTime));
        seq.AppendCallback(() =>
        {
            _fireImg.fillAmount = 0;
        });
    }

    IEnumerator BurnningLoop()
    {
        _fireImg.DOFade(1, _easingTime);
        _fireImg.fillAmount = 0;
        while (_fireImg.fillAmount < 1)
        {
            _fireImg.fillAmount += 0.1f;
            
            yield return new WaitForSeconds(0.02f);
        }

        fireSequence = DOTween.Sequence();
        
        for(int i = 0; i < _gradationColor.Length; i++)
        {
            fireSequence.Append(_fireImg.DOColor(_gradationColor[i], _easingTime));
        }
        fireSequence.SetLoops(-1, LoopType.Yoyo);
    }
}
