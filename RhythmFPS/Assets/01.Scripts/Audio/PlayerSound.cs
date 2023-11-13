using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : AudioPlayer
{
    [SerializeField]private AudioClip _dashClip;
    [SerializeField]private AudioClip _deathClip;
    [SerializeField]private AudioClip _hitClip;
    

    public void PlayerDashClip()
    {
        PlayWithVariablePitch(_dashClip,3f);
    }
    public void PlayerDeathClip()
    {
        PlayWithBasePitch(_deathClip);
    }
    public void PlayerHitClip()
    {
        PlayWithBasePitch(_hitClip);
    }

}
