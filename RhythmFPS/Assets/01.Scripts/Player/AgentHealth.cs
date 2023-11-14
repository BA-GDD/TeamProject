using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentHealth : MonoBehaviour,IDamageable
{
    protected float _curHP;
    [SerializeField]protected float _maxHP;

    public float CurHP => _curHP;
    public float MaxHP => _maxHP;

    public virtual void Die()
    {
        Destroy(gameObject);
    }

    public virtual void TakeDamage(float damage)
    {
        _curHP -= damage;
        if(_curHP <= 0)
        {
            _curHP = 0;
            Die();
        }
    }

    protected virtual void Init(int hp)
    {
        _curHP = _maxHP = hp;
    }
}
