using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBrain : MonoBehaviour
{
    public Transform targetTrm;
    public Vector3 movePos;
    public bool isMove;
    public bool isRot;
    public EnemyHealth enemyHealth;

    public abstract void Attack();
    public abstract void Move();

    protected virtual void Awake()
    {
        isRot = true;
        movePos = transform.position;
        enemyHealth = GetComponent<EnemyHealth>();
        targetTrm = SubManager.Instance.playerTrm;
    }

    protected virtual void Update()
    {
        Move();
    }
}
