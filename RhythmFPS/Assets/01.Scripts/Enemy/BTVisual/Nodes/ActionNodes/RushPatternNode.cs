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
        // ���߿� CollisionCheck Invoke Repeat���� �ʴ� 10�� ������ �ٿ������
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
        // �÷��̾� �浹 üũ
        Collider[] playerChecks = Physics.OverlapSphere((brain as BossBrain).shield.transform.position, 3f, _playerLayerMask);
        if (playerChecks.Length > 0)
        {
            playerChecks[0].GetComponent<IDamageable>().TakeDamage(18);
            (brain as BossBrain).BossAnimator.OnAnimationPlay();
            return State.SUCCESS;
        }

        // ���� �� ������ ��� �ߴµ� collisionCheckPos ��ġ�� ���� radius�� �ʹ� ŭ
        // �� �浹 üũ
        Collider[] groundChecks = Physics.OverlapSphere((brain as BossBrain).collisionCheckPos.position, 1f, _groundLayerMask);
        if (groundChecks.Length > 0)
        {
            (brain as BossBrain).BossAnimator.OnAnimationPlay();
            return State.SUCCESS;
        }
        return State.RUNNING;
    }
}
