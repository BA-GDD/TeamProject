using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RythmTaker : MonoBehaviour
{
    private void Start()
    {
        int rand = Time.frameCount % 2 == 0 ? 1 : -1;
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOScale(new Vector3(1, 1, 1), 0.5f));
        seq.Join(transform.DOLocalRotate(new Vector3(0, 0, rand * 180), 0.5f));
        seq.AppendCallback(() =>
        {
            Destroy(gameObject);
        });
    }
}
