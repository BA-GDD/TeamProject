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
        _targetPos = (GameManager.instance.playerTransform.position - brain.transform.position).normalized * 10f;
        
    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {
        /*if (brain.isMove)
        {
            return State.RUNNING;
        }
        else
        {
            RaycastHit hit;
            bool isHit = Physics.SphereCast(brain.transform.position, 10f, Vector3.forward, out hit, 0f, _layerMask);

            if (isHit)
            {
                //brain.movePos = hit.point;
                //return State.SUCCESS;

                //플레이어 피격 실행
                hit.collider.GetComponent<AgentHealth>().TakeDamage(30);
                return State.SUCCESS;
            }
            if (brain.transform.position != _targetPos)
            {
                brain.moveDestination = _targetPos;
                brain.Move();
                return State.RUNNING;
            }
            //0.375 sec*/
            return State.SUCCESS;
        //}
    }
}
