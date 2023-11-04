using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentWeapon : MonoBehaviour
{
    [SerializeField]private Weapon _curWeapon;

    private void Awake()
    {
        _curWeapon?.Init();
    }
    public void Active()
    {
        if (RhythmManager.instance.Judgement(RhythmAction.Shoot) == false) return;

        _curWeapon?.Fire();
    }
    public void Reload()
    {
        if (RhythmManager.instance.Judgement(RhythmAction.Shoot) == false) return;

        _curWeapon?.Reload();
    }
    public void ChangeWeapon(Weapon newWeapon)
    {
        _curWeapon = newWeapon;
    }
}
