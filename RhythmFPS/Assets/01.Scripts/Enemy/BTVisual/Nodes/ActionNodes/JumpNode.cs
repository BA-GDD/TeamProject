using BTVisual;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;

public class JumpNode : ActionNode
{
    private Vector3 _targetPos;
    [SerializeField]
    private LayerMask _groundLayerMask;
    private float _jumpPower = 5f;
    private bool _alreadyJump = false;

    protected override void OnStart()
    {
        //(brain as BossBrain).isMove = false;
        brain.gameObject.GetComponent<NavMeshAgent>().enabled = false;
        Debug.Log($"온 스타트에서: {brain.gameObject.GetComponent<NavMeshAgent>().enabled}");
        _targetPos = GameManager.instance.PlayerTransform.position;
    }

    protected override void OnStop()
    {
        _alreadyJump = false;
        brain.gameObject.GetComponent<NavMeshAgent>().enabled = true;
        Debug.Log($"온 스탑에서: {brain.gameObject.GetComponent<NavMeshAgent>().enabled}");
        //(brain as BossBrain).isMove = true;
    }

    protected override State OnUpdate()
    {
        if (!_alreadyJump)
        {
            Debug.Log($"업데이트: {brain.gameObject.GetComponent<NavMeshAgent>().enabled}");
            (brain as BossBrain).transform.LookAt(GameManager.instance.PlayerTransform);
            (brain as BossBrain).transform.DOJump(_targetPos, _jumpPower + 20, 1, 1.7f);
            _alreadyJump = true;
        }
        if(brain.transform.position != _targetPos)
        {
            return State.RUNNING;
        }

        return State.SUCCESS;
    }
}
