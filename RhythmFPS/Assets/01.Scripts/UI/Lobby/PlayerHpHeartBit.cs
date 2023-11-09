using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerHpHeartBit : MonoBehaviour
{
    [SerializeField] private Image _heartbitLine;
    [SerializeField] private Transform _heart;

    private void Start()
    {
        HeartBitStart(1);
    }

    public void HeartBitStart(float metronomTurm)
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(_heart.DOScale(1.1f, 0.2f));
        seq.AppendCallback(() =>
        {
            _heart.localScale = Vector3.one;

        });
        seq.Join(_heart.DOLocalRotate(Vector3.zero, 0.2f));
        seq.SetDelay(metronomTurm - 0.4f);
        seq.SetLoops(-1);
    }

    private void Update()
    {
        if (Time.frameCount % 4 == 0)
        {
            if (_heartbitLine.fillAmount >= 1)
            {
                _heartbitLine.fillAmount = 0;
            }
            _heartbitLine.fillAmount += 0.01f;
        }
    }
}