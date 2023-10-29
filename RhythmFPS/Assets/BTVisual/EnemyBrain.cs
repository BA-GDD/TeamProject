using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBrain : MonoBehaviour
{
    public Transform targetTrm;
    public Vector3 movePos;
    public bool isMove;

    public abstract void Attack();
    public abstract void Move();

    protected virtual void Awake()
    {
        movePos = transform.position;
    }

    protected virtual void Update()
    {
        Move();
    }
}
