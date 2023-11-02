using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleWeapon : Weapon
{
    public BulletTrail trailPrefab;
    public Transform firePos;
    public Metronome metronome;

    public override void Fire()
    {
        if (metronome.Judgement() == false)
        {
            print("안맞음!");
            return;
        }
        print("맞음!");
        BulletTrail trail = Instantiate(trailPrefab);
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, 50f, whatIsEnemy))
        {
            if (hit.transform.TryGetComponent<IDamageable>(out IDamageable damageable))
            {
                damageable.TakeDamage(5);
            }
            trail.DrawTrail(firePos.position, hit.point, 0.1f);
        }
        else
        {
            trail.DrawTrail(firePos.position, cam.transform.forward*500f, 0.1f);
        }
    }
}
