using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleWeapon : Weapon
{
    public BulletTrail _trailPrefab;
    public Transform _firePos;
    public Metronome metronome;

    public override void Fire()
    {
        if (metronome.Judgement() == false)
        {
            print("안맞음!");
            return;
        }
        print("맞음!");
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
    }
}
