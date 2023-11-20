using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAttack : MonoBehaviour
{
    protected EnemyBrain _brain;
    protected float _attackTimer;
    protected EnemySound _soundPlayer;

    protected virtual void Awake()
    {
        _brain = transform.parent.GetComponent<EnemyBrain>();
        _attackTimer = _brain.status.attackDelay;
    }

    protected virtual void Update()
    {
        _attackTimer += Time.deltaTime;
    }

    public abstract void Attack();
    public virtual void OnDieEvent()
    {
        PoolManager.Instance.Push(_brain);
    }
}
