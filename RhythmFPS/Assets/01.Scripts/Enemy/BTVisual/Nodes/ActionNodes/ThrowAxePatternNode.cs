using BTVisual;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowAxePatternNode : ActionNode
{
    public GameObject axePrefab;

    protected override void OnStart()
    {

    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {
        GameObject thrownAxe = Instantiate(axePrefab, brain.transform.position, Quaternion.identity);
        return State.SUCCESS;
    }
}
