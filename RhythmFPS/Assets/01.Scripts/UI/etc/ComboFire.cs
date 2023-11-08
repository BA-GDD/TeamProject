using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ComboFire : MonoBehaviour
{
    [SerializeField] private Color[] _gradationColor;
    [SerializeField] private float _easingTime;
    private Image _fireImg;

    private void Awake()
    {
        _fireImg = GetComponent<Image>();
    }

    private void Start()
    {
        Sequence seq = DOTween.Sequence();

        for(int i = 0; i < _gradationColor.Length; i++)
        {
            seq.Append(_fireImg.DOColor(_gradationColor[i], _easingTime));
        }
        seq.SetLoops(-1);
    }
}
