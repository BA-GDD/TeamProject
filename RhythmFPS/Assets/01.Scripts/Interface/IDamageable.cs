using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public interface IDamageable
{
    public void TakeDamage(float damage);
    public void TakeDamage(float damage,Vector3 point, Vector3 normal);
    public void Die();
}
