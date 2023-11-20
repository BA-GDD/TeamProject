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
    [SerializeField] private float _rushSpeed;

    protected override void OnStart()
    {
        (brain as BossBrain).isMove = false;
        (brain as BossBrain).BossAnimator.SetAttackTrigger(true);
        (brain as BossBrain).isCanAttack = false;
        _targetDir = (GameManager.instance.PlayerTransform.position - brain.transform.position).normalized;

    }

    protected override void OnStop()
    {
        (brain as BossBrain).isMove = true;
    }

    protected override State OnUpdate()
    {
        // 나중에 CollisionCheck Invoke Repeat으로 초당 10번 정도로 줄여줘야함
        State state = CollisionCheck();
        if(state != State.SUCCESS)
        {
            (brain as BossBrain).transform.position += _targetDir * _rushSpeed * Time.deltaTime;
        }
        (brain as BossBrain).timer = 0;
        return state;
    }

    private State CollisionCheck()
    {
        // 플레이어 충돌 체크
        Collider[] playerChecks = Physics.OverlapSphere((brain as BossBrain).shield.transform.position, 3f, _playerLayerMask);
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
}
