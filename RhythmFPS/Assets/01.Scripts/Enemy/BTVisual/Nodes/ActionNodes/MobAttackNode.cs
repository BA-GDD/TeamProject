using BTVisual;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobAttackNode : ActionNode
{
    protected override void OnStart()
    {
        brain.StopChase();
        (brain as MobBrain).Animator.SetIsMove(false);
        (brain as MobBrain).Animator.SetIsAttack(true);
    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {
        Vector3 lookRotation = GameManager.instance.PlayerTransform.position - brain.transform.position;
        lookRotation.y = 0f;
        brain.transform.rotation = Quaternion.Slerp(brain.transform.rotation, Quaternion.LookRotation(lookRotation), Time.deltaTime * 10f);

        brain.Attack();

        return State.SUCCESS;
    }
}
