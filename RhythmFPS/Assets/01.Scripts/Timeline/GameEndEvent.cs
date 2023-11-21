using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class GameEndEvent : MonoBehaviour
{
    [SerializeField]
    private PlayableDirector _director;

    [SerializeField]
    private AudioSource _audioSource;
   

    public void DirectorEndEvent(PlayableDirector director)
    {
        //���� �߰��ϸ� �ȴ�.

        GameManager.instance.gameObject.BroadcastMessage("PushObject");
        GameManager.instance.GameClear();
        _audioSource.Play();
        print("���� ��");
    }
    public void GameEnd()
    {
        GameManager.instance.PlayerTransform.GetComponent<PlayerHealth>().isCanHit = false;
        _director.stopped += DirectorEndEvent;
        _audioSource.Pause();
        _director.Play();
    }
}
