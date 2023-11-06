using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using MoreMountains;
using MoreMountains.Feedbacks;

public class EffectPlayer : MonoBehaviour
{
    public List<ParticleSystem> fx = new List<ParticleSystem>();
    public MMF_Player player;

    private void Update()
    {
        if (Keyboard.current.gKey.wasPressedThisFrame)
        {
            EffectPlay();
        }
    }

    public void EffectPlay()
    {
        fx.ForEach(e => e.Play());
        player.PlayFeedbacks();
    }
}
