using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Core;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public PoolingListSO poolingList;

    private Transform _playerTransform;
    public Transform PlayerTransform 
    { get
        {
            if(_playerTransform == null)
                _playerTransform = FindAnyObjectByType<AgentController>().transform;
            if (_playerTransform == null)
                Debug.LogError("player Is Not have this Scene");//영어 알빠노
            return _playerTransform;
        }
    }

    public DifficultyType difficult;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("GameManager instance is already exist!");
        }

        instance = this;

        PoolManager.Instance = new PoolManager(transform);
        foreach(PoolingItem item in poolingList.PoolList)
        {
            PoolManager.Instance.CreatePool(item.Prefab, item.Count);
        }
        DontDestroyOnLoad(gameObject);
        //Debug
        
    }

    private void Update()
    {
        if (Keyboard.current.kKey.wasPressedThisFrame)
        {
            GameStart(DifficultyType.normal);
        }
    }
    /// <summary>
    /// ���� ���� �۾�
    /// </summary>
    public void GameStart(DifficultyType stageDifficult)
    {
        difficult = stageDifficult;
        SceneManager.LoadScene(SceneNames.main.ToString());
        //playerTransform = FindAnyObjectByType<AgentController>().transform;

        //if (!playerTransform)
        //{
        //    Debug.LogError("Any player is not exist in game!");
        //}
    }

    /// <summary>
    /// ���� ���� �۾�
    /// </summary>
    public void GameEnd()
    {

    }

    /// <summary>
    /// ���� ����� �۾�
    /// </summary>
    public void GameRestart()
    {
        //����� �� �ʿ��� �� �ʱ�ȭ
    }
}
