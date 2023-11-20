using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySound : AudioPlayer
{
    public void PlayAttackSound(AudioClip clip)
    {
        PlayWithVariablePitch(clip);
    }
    public void PlayIdleSound(AudioClip clip)
    {
        PlayWithBasePitch(clip);
    }
}
