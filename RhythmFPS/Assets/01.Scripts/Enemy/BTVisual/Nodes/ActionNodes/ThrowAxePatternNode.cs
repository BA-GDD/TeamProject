using BTVisual;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowAxePatternNode : ActionNode
{
    public GameObject axePrefab;
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
            (brain as BossBrain).BossAnimator.OnAnimationTrigger += OnThrowAxeHandle;
            (brain as BossBrain).BossAnimator.SetMove(false);
            (brain as BossBrain).BossAnimator.SetAttackTrigger(true);
            (brain as BossBrain).IsCanAttack = false;
            _alreadyInit = true;
        }
    }

    protected override void OnStop()
    {
        RhythmManager.instance.onNotedTimeAction -= OnPatternInitHandle;
        (brain as BossBrain).BossAnimator.OnAnimationTrigger -= OnThrowAxeHandle;
        //(brain as BossBrain).BossAnimator.SetAttackTrigger(false);
        (brain as BossBrain).BossAnimator.SetAnimationClipEndState(false);
        (brain as BossBrain).IsMove = true;
        (brain as BossBrain).IsCanAttack = true;
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

    private void OnThrowAxeHandle()
    {
        GameObject thrownAxe = Instantiate(axePrefab, (brain as BossBrain).Weapon.transform.position, Quaternion.identity);
    }
}
