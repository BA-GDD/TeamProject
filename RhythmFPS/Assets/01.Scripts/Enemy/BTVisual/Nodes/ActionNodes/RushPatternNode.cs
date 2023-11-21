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
        // ���߿� CollisionCheck Invoke Repeat���� �ʴ� 10�� ������ �ٿ������
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
        // �÷��̾� �浹 üũ
        Collider[] playerChecks = Physics.OverlapSphere((brain as BossBrain).Shield.transform.position, 3f, _playerLayerMask);
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

    public void RushStartHandle()
    {
        _rushStart = true;
    }
}
