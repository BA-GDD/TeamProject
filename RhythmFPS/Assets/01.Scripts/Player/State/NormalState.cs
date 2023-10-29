using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalState : CommonState
{
    public override void OnEnterState()
    {
        inputReader.JumpEvent += OnJumpDownHandle;
        inputReader.MovementEvent += OnMovementHandle;
        inputReader.RotationCameraEvt += OnRotateHandle;
        inputReader.FireEvnet += OnFireHandle;
    }
    private void OnFireHandle()
    {
        agentWeapon?.Active();
    }
    private void OnRotateHandle(Vector2 value)
    {
        agentRotater?.Rotate(value);
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
        inputReader.JumpEvent -= OnJumpDownHandle;
        inputReader.MovementEvent -= OnMovementHandle;
        inputReader.RotationCameraEvt -= OnRotateHandle;
        inputReader.FireEvnet -= OnFireHandle;
    }


    public override void SetUp(Transform agentRoot,InputReader inputReader)
    {
        base.SetUp(agentRoot,inputReader);
    }

    public override void UpdateState()
    {
    }
}
