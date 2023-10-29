using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentHealth : MonoBehaviour
{
    protected int _curHP;
    protected int _maxHP;

    public int CurHP => _curHP;
    public int MaxHP => _maxHP;

    protected virtual void Init(int hp)
    {
        _curHP = _maxHP = hp;
    }
}
