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
        _brain.SetDead();
    }

    public void TakeDamage(int damage)
    {
        Debug.Log($"보스 남은 체력: {_currentHitPoint}");
        
        _currentHitPoint = Mathf.Clamp(_currentHitPoint - damage, 0, _brain.status.maxHitPoint);
        UIManager.Instanace.HandleBossGetDamage(damage);
        onHitTrigger?.Invoke();
        if(_currentHitPoint <= 0)
        {
            Die();
        }
    }
}
