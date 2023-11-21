using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class SoundPlay : PlayableBehaviour
{
    public AudioPlayer player;
    public AudioClip clip;

    public override void PrepareFrame(Playable playable, FrameData info)
    {
        player.PlayWithBasePitch(clip);
    }
}
