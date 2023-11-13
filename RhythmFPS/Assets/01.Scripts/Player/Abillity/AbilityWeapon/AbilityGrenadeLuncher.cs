using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityGrenadeLuncher : AbilityWeapon
{
    [SerializeField] private Transform _firePos;
    [SerializeField] private GrenadeBullet _bulletPrefab;

    public override void Action()
    {
        Fire();
    }
    public void Fire()
    {
        GrenadeBullet grenadeBullet = PoolManager.Instance.Pop(_bulletPrefab.name) as GrenadeBullet;
        grenadeBullet.transform.position = _firePos.position;
        grenadeBullet.Fire(_firePos.forward);
    }
}
