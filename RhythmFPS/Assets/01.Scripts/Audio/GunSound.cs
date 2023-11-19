using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSound : AudioPlayer
{
    public AudioClip fireClip;
    public AudioClip reloadClip;
    public AudioClip lackOfAmmoClip;

    public void PlayFireSound()
    {
        PlayWithBasePitch(fireClip);
    }
    public void PlayReloadSound()
    {
        PlayWithBasePitch(reloadClip);
    }
    public void PlayLackOfAmmoSound()
    {
        PlayWithVariablePitch(lackOfAmmoClip);
    }
}
