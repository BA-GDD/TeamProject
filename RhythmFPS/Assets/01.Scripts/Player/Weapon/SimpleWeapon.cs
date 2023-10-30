using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleWeapon : Weapon
{
    public override void Fire()
    {
        print("�����~~ ����!");
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, 50f, whatIsEnemy))
        {
            //Ÿ�̹� ���߱�
            hit.transform.GetComponent<IDamageable>().TakeDamage(5);
        }
    }
}
