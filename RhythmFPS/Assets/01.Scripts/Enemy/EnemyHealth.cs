using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    public UnityEvent onHitTrigger;
    public UnityEvent onDieTrigger;
    private EnemyBrain _brain;
    private float _currentHitPoint;
    public float CurrentHitPoint => _currentHitPoint;

    private void Awake()
    {
        _brain = GetComponent<EnemyBrain>();
        _currentHitPoint = _brain.status.maxHitPoint;
    }

    public void Die()
    {
        onDieTrigger?.Invoke();
        Debug.Log("보스 죽음");
        //_brain.SetDead();
        //(_brain as BossBrain).BossAnimator.SetDead();
    }

    public void TakeDamage(float damage)
    {
        _currentHitPoint -= damage;
        Debug.Log($"남은 체력: {_currentHitPoint}");
        onHitTrigger?.Invoke();
        UIManager.Instanace.HandleBossGetDamage(damage);
        if(_currentHitPoint <= 0)
        {
            Die();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            _currentHitPoint = 0;
        }
    }
}
