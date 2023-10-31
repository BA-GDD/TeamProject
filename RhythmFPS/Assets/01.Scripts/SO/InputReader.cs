using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static Controls;

[CreateAssetMenu(menuName = "SO/Input/Reader", fileName = " New Input reader")]
public class InputReader : ScriptableObject, IPlayerActions
{
    public event Action FireEvnet;
    public event Action JumpEvent;
    public event Action<Vector2> MovementEvent;
    public event Action<Vector2> RotationCameraEvt;

    private Controls _controlAction;


    private void OnEnable()
    {
        if (_controlAction == null)
        {
            _controlAction = new Controls();
            _controlAction.Player.SetCallbacks(this);
        }
        _controlAction.Player.Enable();
    }
    
    public void OnMovement(InputAction.CallbackContext context)
    {
        Vector2 inputVec = context.ReadValue<Vector2>();
        MovementEvent?.Invoke(inputVec);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            JumpEvent?.Invoke();
        }
    }
    public void OnFire(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            FireEvnet?.Invoke();
        }
    }

    public void OnMouseDelta(InputAction.CallbackContext context)
    {
        Vector2 delta = context.ReadValue<Vector2>();
        RotationCameraEvt?.Invoke(delta);
    }

}
