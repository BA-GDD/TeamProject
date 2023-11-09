using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BTVisual;
using Core; 

public class GroundSlamPatternNode : ActionNode
{
    public LayerMask ground;
    public LayerMask player;
    protected override void OnStart()
    {
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        RaycastHit hit;
        bool isHit = Physics.Raycast(brain.transform.position, Vector3.down * 100.0f, out hit, 1000.0f, ground);
        Debug.DrawRay(brain.transform.position, Vector3.down * 100.0f, Color.white);
        if (isHit)
        {
            Debug.Log("asd");
            brain.moveDestination = hit.point;
            brain.canRotate = false;
            Vector3 pos = brain.targetTransform.position - brain.transform.position;
            pos.x = 0;
            pos.z = 0;
            brain.transform.rotation = Quaternion.LookRotation(pos);

            RaycastHit[] hits = Physics.SphereCastAll(hit.point, 3.0f, Vector3.up, 10.0f,player);

            if (hits.Length > 0)
            {
                foreach (var h in hits)
                {
                    h.transform.TryGetComponent<IDamageable>(out IDamageable i);
                    if (i != null)
                    {
                        i.TakeDamage(10);
                    }
                }
            }
        }
        blackboard.curPattern = Random.Range(0, 3);
        return State.SUCCESS;
    }
}
