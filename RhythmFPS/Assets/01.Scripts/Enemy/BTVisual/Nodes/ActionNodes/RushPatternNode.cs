using BTVisual;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RushPatternNode : ActionNode
{
    [SerializeField] private LayerMask _layerMask;
    private Vector3 _targetPos;

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
        return State.SUCCESS;
    }

    private void OnDamageCastHandle()
    {
        RaycastHit hit;
        if (Physics.SphereCast(brain.transform.position, 5f, Vector3.forward, out hit, 0f, _layerMask))
        {
            if (hit.collider.TryGetComponent<AgentHealth>(out AgentHealth health))
            {
                health.TakeDamage(20);
                Debug.Log("Rush Pattern is hit");
            }
            else
            {
                Debug.Log("Rush Pattern is not hit");
            }
        }
    }
}
