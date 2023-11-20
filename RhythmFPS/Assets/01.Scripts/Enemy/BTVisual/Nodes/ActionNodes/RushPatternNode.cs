using BTVisual;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RushPatternNode : ActionNode
{
    [SerializeField] private LayerMask _playerLayerMask;
    [SerializeField] private LayerMask _groundLayerMask;
    private Vector3 _targetDir;
    private bool _rushStart;
    [SerializeField] private float _rushSpeed;
    private float _timer = 0f;
    private float _maxTimer = 1.5f;

    protected override void OnStart()
    {
        (brain as BossBrain).IsMove = false;
        (brain as BossBrain).BossAnimator.SetAttackTrigger(true);
        (brain as BossBrain).IsCanAttack = false;
        _targetDir = (GameManager.instance.PlayerTransform.position - brain.transform.position).normalized;
        brain.gameObject.GetComponent<NavMeshAgent>().enabled = false;
        (brain as BossBrain).BossAnimator.OnAnimationTrigger += RushStartHandle;
    }

    protected override void OnStop()
    {
        (brain as BossBrain).IsMove = true;
        brain.gameObject.GetComponent<NavMeshAgent>().enabled = true;
        _rushStart = false;
        (brain as BossBrain).BossAnimator.OnAnimationTrigger -= RushStartHandle;
    }

    protected override State OnUpdate()
    {
        // 나중에 CollisionCheck Invoke Repeat으로 초당 10번 정도로 줄여줘야함
        State state = CollisionCheck();
        if(state != State.SUCCESS)
        {
            if (_rushStart)
            {
                _timer += Time.deltaTime;
                Debug.Log(_timer);
                (brain as BossBrain).transform.position += _targetDir * _rushSpeed * Time.deltaTime;
            }
        }
        if (_timer >= _maxTimer)
        {
            state = State.SUCCESS;
            _timer = 0f;
            (brain as BossBrain).BossAnimator.OnAnimationPlay();
        }
        (brain as BossBrain).timer = 0;
        return state;
    }

    private State CollisionCheck()
    {
        // 플레이어 충돌 체크
        Collider[] playerChecks = Physics.OverlapSphere((brain as BossBrain).Shield.transform.position, 3f, _playerLayerMask);
        if (playerChecks.Length > 0)
        {
            playerChecks[0].GetComponent<IDamageable>().TakeDamage(18);
            (brain as BossBrain).BossAnimator.OnAnimationPlay();
            return State.SUCCESS;
        }

        // 지금 벽 감지가 계속 뜨는데 collisionCheckPos 위치에 비해 radius가 너무 큼
        // 벽 충돌 체크
        Collider[] groundChecks = Physics.OverlapSphere((brain as BossBrain).collisionCheckPos.position, 1f, _groundLayerMask);
        if (groundChecks.Length > 0)
        {
            (brain as BossBrain).BossAnimator.OnAnimationPlay();
            return State.SUCCESS;
        }
        return State.RUNNING;
    }

    public void RushStartHandle()
    {
        _rushStart = true;
    }
}
