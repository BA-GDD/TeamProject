using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentHealth : MonoBehaviour,IDamageable
{
    protected int _curHP;
    protected int _maxHP;

    public int CurHP => _curHP;
    public int MaxHP => _maxHP;

    public virtual void Die()
    {
        Destroy(gameObject);
    }

    public virtual void TakeDamage(int damage)
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
