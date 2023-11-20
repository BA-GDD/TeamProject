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
    private bool _alreadyInit = false;

    protected override void OnStart()
    {
        RhythmManager.instance.onNotedTimeAction += OnPatternInitHandle;
    }

    void OnPatternInitHandle()
    {
        if (!_alreadyInit)
        {
            (brain as BossBrain).IsMove = false;
            (brain as BossBrain).BossAnimator.OnAnimationTrigger += OnDamageCastHandle;
            (brain as BossBrain).BossAnimator.SetAttackTrigger(true);
            (brain as BossBrain).IsCanAttack = false;
            (brain as BossBrain).IsCanAttack = false;
            _alreadyInit = true;
        }
    }

    protected override void OnStop()
    {
        RhythmManager.instance.onNotedTimeAction -= OnPatternInitHandle;
        (brain as BossBrain).BossAnimator.OnAnimationTrigger -= OnDamageCastHandle;
        //(brain as BossBrain).BossAnimator.SetAttackTrigger(false);
        (brain as BossBrain).BossAnimator.SetAnimationClipEndState(false);
        (brain as BossBrain).IsMove = true;
        _alreadyInit = false;
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
        Collider[] colliders = Physics.OverlapSphere((brain as BossBrain).Weapon.transform.position, 3f, _playerLayerMask);
        if (colliders.Length > 0)
        {
            colliders[0].GetComponent<IDamageable>().TakeDamage(14);
            Debug.Log($"베기에 맞았고 남은 체력: {colliders[0].GetComponent<AgentHealth>().CurHP}");
        }
    }

}
