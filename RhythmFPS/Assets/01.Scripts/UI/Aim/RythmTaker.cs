using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RythmTaker : MonoBehaviour
{
    private void Start()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOScale(new Vector3(0.8f, 0.8f, 1), 0.375f));
        seq.Join(transform.DOLocalRotate(new Vector3(0, 0, 90), 0.375f));
        seq.AppendCallback(() =>
        {
            Destroy(gameObject); // 풀매니저 연결
        });
    }
}
