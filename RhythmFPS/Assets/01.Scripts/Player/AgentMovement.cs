using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentMovement : MonoBehaviour
{
    private Vector2 _inputVec;
    private Vector3 _dirVec;

    private CharacterController _characterController;

    private float _yVelocity;

    public float Speed;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }
    public void OnMovementHandle(Vector2 dir)
    {
        _inputVec = dir;
    }
    public void Jump()
    {
        if (_characterController.isGrounded)
        {
            _yVelocity = 7f;
        }
    }
    private void FixedUpdate()
    {
        CalculateMovement();

        _characterController.Move(_dirVec*Time.fixedDeltaTime);
    }
    private void CalculateMovement()
    {
        if (!_characterController.isGrounded)
        {
            _yVelocity -= 9.8f * Time.fixedDeltaTime;
        }
        else
        {
        }
        _dirVec = (_inputVec.x * transform.right + _inputVec.y * transform.forward) * Speed;
        _dirVec.y = _yVelocity;
    }
}
