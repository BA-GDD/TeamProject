using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class NormalState : CommonState
{
    public override void OnEnterState()
    {
    }
    
    

    private void OnJumpDownHandle()
    {
        agentMovement?.Jump();
    }
    private void OnMovementHandle(Vector2 dir)
    {
        agentMovement?.OnMovementHandle(dir);
    }

    public override void OnExitState()
    {
    }


    public override void SetUp(Transform agentRoot,InputReader inputReader)
    {
        base.SetUp(agentRoot,inputReader);
    }

    public override void UpdateState()
    {
    }
}
