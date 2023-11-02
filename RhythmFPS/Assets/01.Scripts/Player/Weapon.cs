using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;

public abstract class Weapon : MonoBehaviour
{
    protected int _maxBullet;
    protected int _currentBullet;
    protected Camera _cam;
    [SerializeField] protected LayerMask _whatIsEnemy;
    
    public virtual void Init()
    {
        _cam = Define.MainCam;
    }
    public abstract void Fire();
    public abstract void Reload();
}
