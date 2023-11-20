using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSound : AudioPlayer
{
    [SerializeField] private AudioClip _clip;

    private void OnEnable()
    {
        PlayEffectSound();
    }

    public void PlayEffectSound()
    {
        PlayWithBasePitch(_clip);
    }
}
