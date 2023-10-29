using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentWeapon : MonoBehaviour
{
    [SerializeField]private Weapon curWeapon;

    private void Awake()
    {
        curWeapon?.Init();
    }
    public void Active()
    {
        curWeapon?.Fire();
    }
    public void ChangeWeapon(Weapon newWeapon)
    {
        curWeapon = newWeapon;
    }
}
