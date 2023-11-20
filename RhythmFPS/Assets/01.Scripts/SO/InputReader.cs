using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static Controls;

[CreateAssetMenu(menuName = "SO/Input/Reader", fileName = " New Input reader")]
public class InputReader : ScriptableObject, IPlayerActions,IUIActions
{
    public event Action fireEvnet;
    public event Action jumpEvent;
    public event Action reloadEvent;
    public event Action<Vector2> movementEvent;
    public event Action<Vector2> rotationCameraEvt;

    public event Action<string> OnAbillityEvent;

    private Controls _controlAction;

    private bool _openSetting;


    private void OnEnable()
    {
        if (_controlAction == null)
        {
            _controlAction = new Controls();
            _controlAction.Player.SetCallbacks(this);
            _controlAction.UI.SetCallbacks(this);
        }
        _controlAction.Player.Enable();
        _controlAction.UI.Enable();
    }
    
    public void OpenSetting(bool isOpen)
    {
        if (isOpen == false)
        {
            _controlAction.Player.Disable();
            GameManager.instance.GamePause();
        }
        else
        {
            _controlAction.Player.Enable();
            GameManager.instance.GameResume();
        }


        _openSetting = !_openSetting;
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        Vector2 inputVec = context.ReadValue<Vector2>();
        movementEvent?.Invoke(inputVec);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            jumpEvent?.Invoke();
        }
    }
    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            fireEvnet?.Invoke();
        }
    }

    public void OnMouseDelta(InputAction.CallbackContext context)
    {
        Vector2 delta = context.ReadValue<Vector2>();
        rotationCameraEvt?.Invoke(delta);
    }

    public void OnReload(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            reloadEvent?.Invoke();

        }
    }

    public void OnAbillity(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            Debug.Log(context.control.displayName);
            OnAbillityEvent?.Invoke(context.control.displayName);
        }
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            UIManager.Instanace.HandleActiveOptionPanel?.Invoke();
        }
    }
}
