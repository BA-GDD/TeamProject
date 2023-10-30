using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentMovement : MonoBehaviour
{
    private Vector2 _inputVec;
    private Vector3 _dirVec;

    private CharacterController _characterController;

    private float _yVelocity;
    private bool _isAir;
    private bool _isGround;

    [SerializeField] private LayerMask _whatIsGround;

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
        if (_isGround)
        {
            _yVelocity = 7f;
        }
    }
    private void FixedUpdate()
    {
        CalculateMovement();

        _characterController.Move(_dirVec * Time.fixedDeltaTime);
    }
    private void CalculateMovement()
    {
        _isGround = Physics.Raycast(transform.position, Vector3.down, 0.08f, _whatIsGround);
        if (!_isGround)
        {
            _yVelocity -= 9.8f * Time.fixedDeltaTime;
            _isAir = true;
        }
        else
        {
            if(_isAir == true)
            {
                _yVelocity = 0;
                _isAir = false;
            }
        }

        _dirVec = (_inputVec.x * transform.right + _inputVec.y * transform.forward) * Speed;
        _dirVec.y = _yVelocity;
    }
}
