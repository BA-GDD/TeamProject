using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static Controls;

[CreateAssetMenu(menuName = "SO/Input/Reader", fileName = " New Input reader")]
public class InputReader : ScriptableObject, IPlayerActions
{
    public event Action fireEvnet;
    public event Action jumpEvent;
    public event Action<Vector2> movementEvent;
    public event Action<Vector2> rotationCameraEvt;

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
        movementEvent?.Invoke(inputVec);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            jumpEvent?.Invoke();
        }
    }
    public void OnFire(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            fireEvnet?.Invoke();
        }
    }

    public void OnMouseDelta(InputAction.CallbackContext context)
    {
        Vector2 delta = context.ReadValue<Vector2>();
        rotationCameraEvt?.Invoke(delta);
    }

}
