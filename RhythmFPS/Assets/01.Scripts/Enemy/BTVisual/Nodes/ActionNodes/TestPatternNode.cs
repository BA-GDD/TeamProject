using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BTVisual;

public class TestPatternNode : ActionNode
{
    public GameObject bullet;

    protected override void OnStart()
    {
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {


        if (brain.isMove)
        {
            return State.RUNNING;
        }
        else
        {
            Debug.Log("Attack");
            for (int i = -1; i < 1; i++)
            {
                GameObject obj = Instantiate(bullet);
                obj.transform.position = brain.transform.position;
                obj.transform.rotation = brain.transform.rotation;
            }

            blackboard.curPattern = Random.Range(0, 3);
            return State.SUCCESS;
        }
    }
}
