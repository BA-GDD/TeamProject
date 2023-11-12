using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class EnterTrigger : MonoBehaviour
{

    //����� ����, ���߿� �Ŵ��� ��������
    [SerializeField]
    private PlayableDirector _director;
    private bool _isStarted = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !_isStarted)
        {
            StageStart();
        }
    }

    /// <summary>
    /// ���������� ������ �� �ϴ� �ൿ���� �־����
    /// </summary>
    private void StageStart()
    {
        _isStarted = true;
        _director.Play();
    }
}
