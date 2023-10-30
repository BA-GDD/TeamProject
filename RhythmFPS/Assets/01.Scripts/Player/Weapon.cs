using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;

public abstract class Weapon : MonoBehaviour
{
    protected int maxBullet;
    protected int currentBullet;
    protected Camera cam;
    [SerializeField] protected LayerMask whatIsEnemy;
    
    public virtual void Init()
    {
        cam = Define.MainCam;
    }
    public abstract void Fire();
}
