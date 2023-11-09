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

    protected override void OnStart()
    {
        brain.BossAnimator.OnAnimationTrigger += OnDamageCastHandle;
    }

    protected override void OnStop()
    {
        brain.BossAnimator.OnAnimationTrigger -= OnDamageCastHandle;
        brain.BossAnimator.SetAttackTrigger(false);
    }

    protected override State OnUpdate()
    {
        if (brain.isMove)
        {
            return State.RUNNING;
        }
        else
        {
            brain.BossAnimator.SetAttackPattern(1);
            brain.BossAnimator.SetAttackTrigger(true);
            return State.SUCCESS;
        }
    }

    private void OnDamageCastHandle()
    {
        RaycastHit hit;

        if (Physics.SphereCast(brain.transform.position, 5f, Vector3.forward, out hit, 3f, _playerLayerMask))
        {
            if (hit.collider.TryGetComponent<AgentHealth>(out AgentHealth health))
            {
                health.TakeDamage(10);
            }
        }
    }
}
