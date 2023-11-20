using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class GameStartTrigger : MonoBehaviour
{
    [SerializeField]
    private PlayableDirector _director;
    [Space(10)]
    [Header("켜줄 오브젝트")]
    [SerializeField]
    private GameObject[] _needActiveObject;
    [Header("꺼줄 오브젝트")]
    [SerializeField]
    private GameObject[] _needInactiveObject;
    private LimitPlayerMovement _limitMovement = null;

    private bool _isStarted = false;    

    private void OnTriggerEnter(Collider other)
    {
        if (_isStarted) return;
        if (other.CompareTag("Player"))
        {
            _limitMovement = other.transform.GetComponent<LimitPlayerMovement>();
            _limitMovement.enabled = true;
            _isStarted = true;
            _director.played += OnStartEvent;
            _director.Play();
            _director.stopped += OnEndEvent;
        }
    }
    private void OnStartEvent(PlayableDirector director)
    {
        UIManager.Instanace.TurnOffAllUI();
    }

    private void OnEndEvent(PlayableDirector director)
    {
        if(_limitMovement != null)
        {
            _limitMovement.enabled = false;
        }

        for(int i = 0; i < _needActiveObject.Length; i++)
        {
            if(_needActiveObject[i] != null)
            {
                _needActiveObject[i].SetActive(true);
            }
        }

        for(int i = 0; i < _needInactiveObject.Length; i++)
        {
            if(_needInactiveObject[i] != null)
            {
                _needInactiveObject[i].SetActive(false);
            }
        }
        UIManager.Instanace.TurnOnAllUI();
    }
}
