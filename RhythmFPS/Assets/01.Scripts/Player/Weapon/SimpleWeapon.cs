using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleWeapon : Weapon
{
    public BulletTrail _trailPrefab;
    public Transform _firePos;

    private void Awake()
    {
        _currentBullet = _maxBullet;
    }

    public override void Fire()
    {

        if (_isReadyReload == true) return;
        if(_currentBullet <= 0)
        {
            lackOfAmmoEvent?.Invoke();
            return;
        }
        print("맞음!");
        _currentBullet--;
        BulletTrail trail = Instantiate(_trailPrefab);
        if (Physics.Raycast(_cam.transform.position, _cam.transform.forward, out RaycastHit hit, 50f, _whatIsEnemy))
        {
            if (hit.transform.TryGetComponent<IDamageable>(out IDamageable damageable))
            {
                damageable.TakeDamage(5);
            }
            trail.DrawTrail(_firePos.position, hit.point, 0.1f);
        }
        else
        {
            trail.DrawTrail(_firePos.position, _cam.transform.forward*500f, 0.1f);
        }
    }

    public override void Reload()
    {
        print("리로드");
        if (_currentBullet == _maxBullet) return;
        if(_isReadyReload == true)
        {
            _currentBullet = _maxBullet;
            _isReadyReload = false;
        }
        else
        {
            _isReadyReload = true;
        }

    }
}
