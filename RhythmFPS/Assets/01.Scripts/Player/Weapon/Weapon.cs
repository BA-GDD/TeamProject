using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;
using UnityEngine.Events;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField]protected int _maxBullet;
    protected int _currentBullet;
    protected Camera _cam;
    protected bool _isReadyReload;

    [SerializeField] protected LayerMask _whatIsEnemy;

    public UnityEvent lackOfAmmoEvent;

    public virtual void Init()
    {
        _cam = Define.MainCam;
    }
    /// <summary>
    ///  실제 총이 발사하는 함수
    /// </summary>
    public abstract void Fire();
    /// <summary>
    ///  실제 총이 발사하는 장전
    /// </summary>
    public abstract void Reload();
}
