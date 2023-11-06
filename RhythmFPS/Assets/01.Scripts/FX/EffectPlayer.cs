using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EffectPlayer : MonoBehaviour
{
    public List<ParticleSystem> fx = new List<ParticleSystem>();

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            EffectPlay();
        }
    }

    public void EffectPlay()
    {
        fx.ForEach(e => e.Play());
    }
}
