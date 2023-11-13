using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RythmTaker : PoolableMono
{
    public float matchTime;
    [SerializeField] private Transform _markLeftTrm;
    [SerializeField] private Transform _markRighttTrm;

    public override void Init()
    {

    }

    private void Start()
    {
        float time = UIManager.Instanace.rhythmTurm;
        Sequence seq = DOTween.Sequence();
        seq.Join(_markLeftTrm.DOLocalMoveX(50, matchTime));
        seq.Join(_markRighttTrm.DOLocalMoveX(-50, matchTime));
        seq.Join(transform.DOScale(0.7f, time));
        seq.AppendCallback(() =>
        {
            Destroy(this.gameObject);
            //PoolManager.Instance.Push(this); // 풀매니저 연결
        });
    }
}
