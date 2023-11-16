using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class SlowMotion : PlayableBehaviour
{
    public float timeValue = 0.3f;

    public override void PrepareFrame(Playable playable, FrameData info)
    {
        Time.timeScale = timeValue;
    }
}
