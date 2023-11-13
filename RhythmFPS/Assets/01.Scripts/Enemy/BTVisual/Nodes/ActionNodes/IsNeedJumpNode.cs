using BTVisual;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsNeedJumpNode : ActionNode
{
    [SerializeField]
    private LayerMask _groundLayerMask;

    protected override void OnStart()
    {

    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {
        if (GameManager.instance.playerTransform.position.y != brain.transform.position.y)
        {

        }
        
        if(!(brain as BossBrain).isOnTheRoof)
        {
            Collider[] colliders = Physics.OverlapSphere((brain as BossBrain).jumpUpCheckPos.position, 1.5f, _groundLayerMask);
            if (colliders.Length > 0
            && Vector3.Distance(brain.transform.position, GameManager.instance.playerTransform.position) >= 6f
            && Mathf.Abs((GameManager.instance.playerTransform.position.y - brain.transform.position.y)) >= 6f)
            {
                //Debug.Log("위로 점프해야함");
                (brain as BossBrain).isOnTheRoof = true;
                return State.SUCCESS;
            }
        }
        else
        {
            Collider[] colliders = Physics.OverlapSphere((brain as BossBrain).jumpUpCheckPos.position, 1f, _groundLayerMask);
            if (colliders.Length <= 0
            && Vector3.Distance(brain.transform.position, GameManager.instance.playerTransform.position) >= 6f
            && Mathf.Abs((GameManager.instance.playerTransform.position.y - brain.transform.position.y)) >= 6f)
            {
                //Debug.Log("아래로 점프해야함");
                (brain as BossBrain).isOnTheRoof = false;
                return State.SUCCESS;
            }
        }
        
        return State.FAILURE;
    }
}
