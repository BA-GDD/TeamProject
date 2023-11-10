using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class EnterTrigger : MonoBehaviour
{

    //디버그 용임, 나중에 매니져 만들어야함
    [SerializeField]
    private PlayableDirector _director;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StageStart();
        }
    }

    /// <summary>
    /// 스테이지가 시작할 때 하는 행동들을 넣어놓음
    /// </summary>
    private void StageStart()
    {
        _director.Play();
    }
}
