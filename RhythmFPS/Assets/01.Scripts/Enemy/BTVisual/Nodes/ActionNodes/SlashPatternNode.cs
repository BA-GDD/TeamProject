using BTVisual;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashPatternNode : ActionNode
{
    // 타격 지점
    private Transform _hitPoint;
    [SerializeField]
    private LayerMask _playerLayerMask;
    private int slashCnt = 0;

    protected override void OnStart()
    {
        (brain as BossBrain).isMove = false;
        (brain as BossBrain).BossAnimator.OnAnimationTrigger += OnDamageCastHandle;
        (brain as BossBrain).BossAnimator.SetAttackTrigger(true);
        (brain as BossBrain).isCanAttack = false;
    }

    protected override void OnStop()
    {
        (brain as BossBrain).BossAnimator.OnAnimationTrigger -= OnDamageCastHandle;
        //(brain as BossBrain).BossAnimator.SetAttackTrigger(false);
        (brain as BossBrain).BossAnimator.SetAnimationClipEndState(false);
        (brain as BossBrain).isMove = true;
    }

    protected override State OnUpdate()
    {
        if((brain as BossBrain).BossAnimator.isAnimationClipEnd)
        {
            return State.SUCCESS;
        }
        (brain as BossBrain).timer = 0;
        return State.RUNNING;
    }

    private void OnDamageCastHandle()
    {
        Collider[] colliders = Physics.OverlapSphere((brain as BossBrain).weapon.transform.position, 3f, _playerLayerMask);
        if (colliders.Length > 0)
        {
            colliders[0].GetComponent<IDamageable>().TakeDamage(14);
            Debug.Log($"베기에 맞았고 남은 체력: {colliders[0].GetComponent<AgentHealth>().CurHP}");
        }
    }

}
