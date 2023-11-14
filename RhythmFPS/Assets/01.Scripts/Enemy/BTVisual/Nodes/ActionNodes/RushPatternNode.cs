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
        Debug.Log("���� ����");
        (brain as BossBrain).isMove = false;
        //(brain as BossBrain).BossAnimator.OnAnimationTrigger += OnDamageCastHandle;
        (brain as BossBrain).BossAnimator.SetMove(false);
        (brain as BossBrain).BossAnimator.SetAttackTrigger(true);
        (brain as BossBrain).isCanAttack = false;
        _targetDir = (GameManager.instance.PlayerTransform.position - brain.transform.position).normalized;
        //(brain as BossBrain).agent.enabled = false;

    }

    protected override void OnStop()
    {
        //(brain as BossBrain).BossAnimator.OnAnimationTrigger -= OnDamageCastHandle;
        //(brain as BossBrain).BossAnimator.SetAttackTrigger(false);
        (brain as BossBrain).BossAnimator.SetAnimationClipEndState(false);
        (brain as BossBrain).isMove = true;
        //(brain as BossBrain).agent.enabled = true;
    }

    protected override State OnUpdate()
    {
        Debug.Log("������ ����");
        // ���߿� CollisionCheck Invoke Repeat���� �ʴ� 10�� ������ �ٿ������
        State state = CollisionCheck();
        if(state != State.SUCCESS)
        {
            Debug.Log("���� ��");
            (brain as BossBrain).transform.position += _targetDir * _rushSpeed * Time.deltaTime;
        }
        (brain as BossBrain).timer = 0;
        return state;
        //(brain as BossBrain).timer = 0;
        //return State.RUNNING;
    }

    private State CollisionCheck()
    {
        // �÷��̾� �浹 üũ
        Collider[] playerChecks = Physics.OverlapSphere((brain as BossBrain).shield.transform.position, 3f, _playerLayerMask);
        if (playerChecks.Length > 0)
        {
            Debug.Log("�÷��̾� ����");
            playerChecks[0].GetComponent<IDamageable>().TakeDamage(18);
            (brain as BossBrain).BossAnimator.OnAnimationPlay();
            return State.SUCCESS;
        }

        // ���� �� ������ ��� �ߴµ� collisionCheckPos ��ġ�� ���� radius�� �ʹ� ŭ
        // �� �浹 üũ
        Collider[] groundChecks = Physics.OverlapSphere((brain as BossBrain).collisionCheckPos.position, 3f, _groundLayerMask);
        if (groundChecks.Length > 0)
        {
            Debug.Log("�� ����");
            (brain as BossBrain).BossAnimator.OnAnimationPlay();
            return State.SUCCESS;
        }
        return State.RUNNING;
    }

    private void OnDamageCastHandle()
    {
        Collider[] colliders = Physics.OverlapSphere((brain as BossBrain).shield.transform.position, 3f, _playerLayerMask);
        if (colliders.Length > 0)
        {
            colliders[0].GetComponent<IDamageable>().TakeDamage(18);
            Debug.Log($"������ �¾Ұ� ���� ü��: {colliders[0].GetComponent<AgentHealth>().CurHP}");
        }
    }
}
