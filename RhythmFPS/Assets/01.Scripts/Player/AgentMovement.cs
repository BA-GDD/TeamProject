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
    private bool _spacialJump;

    [SerializeField] private LayerMask _whatIsGround;

    public float speed;

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
        if (_spacialJump == true)
        {
            _spacialJump = false;
            return;
        }
        if (_isAir)
        {
            _spacialJump = true;
            StartCoroutine(timerSpacialJump(5f));
            return;
        }
        if (_isGround)
        {
            _yVelocity = 7f;
        }
    }
    private IEnumerator timerSpacialJump(float time)
    {
        float timer = 0;
        while(time > timer)
        {
            if (_spacialJump == false) break;
            timer += Time.deltaTime;
            yield return null;
        }
        _spacialJump = false;
    }
    private void Update()
    {
        _isGround = Physics.Raycast(transform.position, Vector3.down, 0.08f, _whatIsGround);
    }
    private void FixedUpdate()
    {
        CalculateMovement();

        _characterController.Move(_dirVec * Time.fixedDeltaTime);
    }
    private void CalculateMovement()
    {
        CalculateYvelocity();

        _dirVec = (_inputVec.x * transform.right + _inputVec.y * transform.forward) * speed;
        _dirVec.y = _yVelocity;
    }
    private void CalculateYvelocity()
    {
        if (!_isGround)
        {
            if(_spacialJump == false)
                _yVelocity -= 9.8f * Time.fixedDeltaTime;
            else
                _yVelocity -= 9.8f * 0.2f * Time.fixedDeltaTime;
            _isAir = true;
        }
        else
        {
            _spacialJump = false;
            if (_isAir == true)
            {
                _yVelocity = 0;
                _isAir = false;
            }
        }
    }
}
