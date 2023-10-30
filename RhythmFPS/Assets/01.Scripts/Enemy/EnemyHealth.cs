using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    public UnityEvent onHitTrigger;
    public UnityEvent onDieTrigger;
    public float curHp = 100;

    public void Die()
    {
        onDieTrigger?.Invoke();
    }

    public void TakeDamage(int damage)
    {
        curHp -= damage;
        onHitTrigger?.Invoke();
        if(curHp <= 0)
        {
            Die();
        }
    }
}
