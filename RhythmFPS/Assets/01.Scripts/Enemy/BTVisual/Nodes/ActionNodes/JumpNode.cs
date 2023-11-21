using BTVisual;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;

public class JumpNode : ActionNode
{
    private Vector3 _targetPos;
    [SerializeField]
    private LayerMask _playerLayerMask;
    private float _jumpPower = 5f;
    private bool _alreadyJump = false;
    [SerializeField]
    private int _damage;
    [SerializeField]
    private DropEffect _explosion;
    private Sequence _jumpSeq;
    private bool _isJump;

    protected override void OnStart()
    {
        //(brain as BossBrain).isMove = false;
        brain.gameObject.GetComponent<NavMeshAgent>().enabled = false;
        _targetPos = GameManager.instance.PlayerTransform.position;
        _isJump = true;
    }

    protected override void OnStop()
    {
        _alreadyJump = false;
        brain.gameObject.GetComponent<NavMeshAgent>().enabled = true;
        //(brain as BossBrain).isMove = true;
        _isJump = false;
    }

    protected override State OnUpdate()
    {
        if (!_alreadyJump)
        {
            Debug.Log($"업데이트: {brain.gameObject.GetComponent<NavMeshAgent>().enabled}");
            (brain as BossBrain).transform.LookAt(GameManager.instance.PlayerTransform);
            _jumpSeq = DOTween.Sequence();
            Transform brainTrm = (brain as BossBrain).transform;
            Vector3 jumpPos = brainTrm.forward * Vector3.Distance(GameManager.instance.PlayerTransform.position,brainTrm.position) *0.5f + Vector3.up * 50;
            _jumpSeq.Append(brainTrm.DOMove(brainTrm.position + jumpPos, 0.5f).SetEase(Ease.OutQuart));
            _jumpSeq.Append(brainTrm.DOMove(_targetPos, 0.8f).SetEase(Ease.InQuart));
            //(brain as BossBrain).transform.DOJump(_targetPos, _jumpPower + 20, 1, 1.7f);
            _alreadyJump = true;
            _jumpSeq.onComplete = () => _isJump = false;
        }
        if (_isJump == true)
        {
            return State.RUNNING;
        }

        DropEffect particle = PoolManager.Instance.Pop(_explosion.name) as DropEffect;
        particle.transform.position = brain.transform.position;


        Collider[] colliders = Physics.OverlapSphere((brain as BossBrain).transform.position, 3f, _playerLayerMask);
        if (colliders.Length > 0)
        {
            colliders[0].GetComponent<IDamageable>().TakeDamage(_damage);
        }
        return State.SUCCESS;
    }
}
