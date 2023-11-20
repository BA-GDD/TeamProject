using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentMovement : MonoBehaviour
{
    const int _jumpCnt = 2;
    private Vector2 _inputVec;
    private Vector3 _dirVec;
    private Vector3 _virtualVec;

    private CharacterController _characterController;
    private PlayerAnimator _animator;

    private bool _isAir;
    private bool _isGround;
    private int _curJumpCnt;
    private float _yVelocity;

    [SerializeField] private LayerMask _whatIsGround;
    [SerializeField] private MMF_Player _feedbackPlayer;

    public float speed;
    public float gravity = 9.8f;
    public bool canMove = true;
    public bool isAddDir;


    #region 프로퍼티
    public Vector3 InputforVec => _inputVec.x * transform.right + _inputVec.y * transform.forward;
    public Vector3 VirtualVec { get => _virtualVec; set { _virtualVec = value; } }
    public float YVelocity { get => _yVelocity; set { _yVelocity = value; } }
    #endregion

    Coroutine virtualVectorCor;
    Coroutine checkWallCor;
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
            if (_isAir == true) _curJumpCnt = 0;
            else _curJumpCnt--;
            if (_curJumpCnt == 0)
            {
                _yVelocity = 8f;
                _virtualVec = Vector3.zero;
            }
            else
                _yVelocity = 5f;
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

        Vector3 vec;
        if (_virtualVec.sqrMagnitude > 0)
        {
            Vector3 saveVector = _dirVec;
            saveVector.y = 0;
            _virtualVec += saveVector * Time.fixedDeltaTime;
            _virtualVec.y -= 9.8f * Time.fixedDeltaTime;
            vec = _virtualVec * Time.fixedDeltaTime;
        }
        else
        {
            vec = _dirVec * Time.fixedDeltaTime;
        }

            _characterController.Move(vec);
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
            _yVelocity -= gravity * Time.fixedDeltaTime;
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

    public void AddInput()
    {
        _dirVec += (_inputVec.x * transform.right + _inputVec.y * transform.forward);
    }
    public void AddForce(Vector3 dir)
    {
        _virtualVec += dir;
        virtualVectorCor = StartCoroutine(SetZeroVirtualVec());
        checkWallCor = StartCoroutine(CheckWallCor());

    }
    private IEnumerator CheckWallCor()
    {
        yield return new WaitUntil(() => _isAir);

        Vector3 rayDir = _virtualVec;
        rayDir.y = 0;
        while (!_isGround)
        {
            Vector3 p1 = transform.position + _characterController.center + Vector3.up * -_characterController.height * 0.25F;
            Vector3 p2 = p1 + Vector3.up * _characterController.height * 0.5f;
            if (Physics.CapsuleCast(p1, p2, _characterController.radius, rayDir, out RaycastHit hit, 0.1f))
            {
                _yVelocity = 0;
                break;
            }
            yield return null;
        }
        _virtualVec = Vector3.zero;
        StopCoroutine(virtualVectorCor);
    }
    private IEnumerator SetZeroVirtualVec()
    {
        yield return new WaitUntil(() => _isAir);
        yield return new WaitUntil(() => _isGround);
        float percent = 0f;
        float time = 0.3f;
        float timer = 0f;
        Vector3 dir = _virtualVec;

        while (percent < 1)
        {
            timer += Time.deltaTime;
            percent = timer / time;

            _virtualVec = Vector3.Lerp(dir, Vector3.zero, percent);
            yield return null;
        }
        _virtualVec = Vector3.zero;

    }

}
