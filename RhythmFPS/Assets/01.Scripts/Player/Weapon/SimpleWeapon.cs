using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleWeapon : Weapon
{
    public override void Fire()
    {
        print("½´¿ì¿ô~~ ½ÇÆÐ!");
        if(Physics.Raycast(cam.transform.position,cam.transform.forward,50f))
        {

        }
    }
}
