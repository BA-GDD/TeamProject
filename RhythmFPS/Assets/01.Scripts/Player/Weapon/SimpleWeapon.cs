using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleWeapon : Weapon
{
    public override void Fire()
    {
        print("슈우웃~~ 실패!");
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, 50f, whatIsEnemy))
        {
            //타이밍 맞추기
            hit.transform.GetComponent<IDamageable>().TakeDamage(5);
        }
    }
}
