using BTVisual;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashPatternNode : ActionNode
{
    // 타격 지점
    private Transform _hitPoint;

    private LayerMask _playerLayerMask;
    private int slashCnt = 0;

    private State _curState = State.RUNNING;

    protected override void OnStart()
    {
        brain.StopChase();
        (brain as BossBrain).BossAnimator.OnAnimationTrigger += OnDamageCastHandle;
    }

    protected override void OnStop()
    {
        (brain as BossBrain).BossAnimator.OnAnimationTrigger -= OnDamageCastHandle;
        //(brain as BossBrain).BossAnimator.SetAttackTrigger(false);
        brain.StartChase();
    }

    protected override State OnUpdate()
    {

        (brain as BossBrain).BossAnimator.SetAttackPattern(1);
        (brain as BossBrain).BossAnimator.SetAttackTrigger(true);
        (brain as BossBrain).isCanAttack = false;
        (brain as BossBrain).timer = 0;
        return _curState;
    }

    private void OnDamageCastHandle()
    {
        RaycastHit hit;

        if (Physics.SphereCast(brain.transform.position, 5f, Vector3.forward, out hit, 3f, _playerLayerMask))
        {
            if (hit.collider.TryGetComponent<AgentHealth>(out AgentHealth health))
            {
                health.TakeDamage(10);
                Debug.Log("Slash Pattern is hit");
            }
            else
            {
                Debug.Log("Slash Pattern is not hit");
            }
        }
    }
}
