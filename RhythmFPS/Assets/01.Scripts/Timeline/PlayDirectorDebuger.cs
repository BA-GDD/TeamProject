using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;


public class PlayDirectorDebuger : MonoBehaviour
{
    private PlayableDirector _director;
    private bool _isPlaying = false;
    private void Awake()
    {
        _director = GetComponent<PlayableDirector>();
    }

    private void Update()
    {
        if (Keyboard.current.kKey.wasPressedThisFrame && !_isPlaying)
        {
            _isPlaying = true;
            _director.Play();   
        }
    }
}
