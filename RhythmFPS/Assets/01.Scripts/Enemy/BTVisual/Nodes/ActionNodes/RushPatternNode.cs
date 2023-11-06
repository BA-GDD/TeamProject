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
        _targetPos = brain.transform.position;
    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {
        RaycastHit hit;
        bool isHit = Physics.SphereCast(brain.transform.position, 10f, Vector3.forward, out hit, 0f, _layerMask);

        if (isHit)
        {
            //brain.movePos = hit.point;
            //return State.SUCCESS;

            //플레이어 피격 실행
            hit.collider.GetComponent<AgentHealth>().TakeDamage(1);
            return State.SUCCESS;
        }
        brain.movePos = _targetPos;
        //0.375 sec
        return State.SUCCESS;
    }
}
