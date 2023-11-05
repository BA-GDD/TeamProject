using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class NormalState : CommonState
{
    public override void OnEnterState()
    {
        inputReader.jumpEvent += OnJumpDownHandle;
        inputReader.movementEvent += OnMovementHandle;
        inputReader.rotationCameraEvt += OnRotateHandle;
        inputReader.fireEvnet += OnFireHandle;
        inputReader.reloadEvent += OnReloadHandle;
        inputReader.OnAbillityEvent += OnAbilityHandle;
    }

    private void OnAbilityHandle(string key)
    {
        StringBuilder builder = new StringBuilder(key);
        builder.Replace(" ", "");
        agentAbility.ActiveAbility(builder.ToString());
    }
    private void OnReloadHandle()
    {
        agentWeapon?.Reload();
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
        inputReader.jumpEvent -= OnJumpDownHandle;
        inputReader.movementEvent -= OnMovementHandle;
        inputReader.rotationCameraEvt -= OnRotateHandle;
        inputReader.fireEvnet -= OnFireHandle;
        inputReader.reloadEvent -= OnReloadHandle;
    }


    public override void SetUp(Transform agentRoot,InputReader inputReader)
    {
        base.SetUp(agentRoot,inputReader);
    }

    public override void UpdateState()
    {
    }
}
