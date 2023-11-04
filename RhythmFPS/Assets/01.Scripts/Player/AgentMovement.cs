using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentMovement : MonoBehaviour
{
    const int _jumpCnt = 2;
    private Vector2 _inputVec;
    private Vector3 _dirVec;

    private CharacterController _characterController;
    private PlayerAnimator _animator;

    private float _yVelocity;
    private bool _isAir;
    private bool _isGround;
    private int _curJumpCnt;

    [SerializeField] private LayerMask _whatIsGround;

    public float speed;
    public bool canMove = true;

    #region 프로퍼티
    public Vector3 InputforVec => _inputVec.x * transform.right + _inputVec.y * transform.forward;
    #endregion
    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = transform.Find("Visual").GetComponent<PlayerAnimator>();
    }
    public void OnMovementHandle(Vector2 dir)
    {
        _inputVec = dir;
    }
    public void Jump()
    {
        if (_curJumpCnt > 0)
        {
            _curJumpCnt--;
            _yVelocity = _curJumpCnt == 0 ? 8f : 5f;
        }
    }
    private void Update()
    {
        _isGround = Physics.Raycast(transform.position, Vector3.down, 0.08f, _whatIsGround);
    }
    private void FixedUpdate()
    {
        if (canMove == false) return;
        CalculateMovement();

        _characterController.Move(_dirVec * Time.fixedDeltaTime);
    }
    private void CalculateMovement()
    {
        CalculateYvelocity();

        _dirVec = (_inputVec.x * transform.right + _inputVec.y * transform.forward) * speed;
        _animator.SetFloatSpeed(_dirVec.sqrMagnitude);
        _dirVec.y = _yVelocity;
    }
    private void CalculateYvelocity()
    {
        if (!_isGround)
        {
            _yVelocity -= 9.8f * Time.fixedDeltaTime;
            _isAir = true;
        }
        else
        {
            if (_isAir == true)
            {

                _yVelocity = 0;
                _curJumpCnt = _jumpCnt;
                _isAir = false;
            }
        }
    }
    public void StopImmediately()
    {
        _dirVec = Vector3.zero;
    }
    public void ManualMove(Vector3 dir)
    {
        _characterController.Move(dir);
    }
}
