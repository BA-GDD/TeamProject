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
    ///  ���� ���� �߻��ϴ� �Լ�
    /// </summary>
    public abstract void Fire();
    /// <summary>
    ///  ���� ���� �߻��ϴ� ����
    /// </summary>
    public abstract void Reload();
}
