using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class RhythmTaker : PoolableMono
{
    [SerializeField] private Image _thisImg;
    private bool _isStart;

    private Vector3 _targetPos;
    private Vector3 _startPos;
    private float _startTime;
    private float _duration;
    [SerializeField] private float _waitFrame;
    private AimRythmer _aimRythmer;

    public void MoveStart(Vector3 targetPos, AimRythmer ar)
    {
        _aimRythmer = ar;
        _startPos = transform.localPosition;
        _targetPos = targetPos;
        _startTime = Time.time;
        _duration = UIManager.Instanace.rhythmTurm * 3;

        _isStart = true;

        Sequence seq = DOTween.Sequence();
        seq.Join(_thisImg.DOFade(1, _duration));
        seq.AppendCallback(() =>
        {
            _aimRythmer.MatChRhythm();
            _isStart = false;
            PoolManager.Instance.Push(this);
        });
    }

    private void Update()
    {
        if (!_isStart) return;

        float progress = (Time.time - _startTime) / _duration;
        progress = Mathf.Clamp(progress, 0, 1);
        Vector3 newPos = (_startPos + (_targetPos - _startPos) * progress) + new Vector3(0, 0.5f, 0);
        transform.localPosition = newPos;
    }

    public override void Init()
    {
        _thisImg.color = new Color(1, 1, 1, 0);
    }
}
