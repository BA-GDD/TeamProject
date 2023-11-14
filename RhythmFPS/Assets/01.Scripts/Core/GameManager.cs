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

        SceneChange(SceneType.inGame);

        //playerTransform = FindAnyObjectByType<AgentController>().transform;

        //if (!playerTransform)
        //{
        //    Debug.LogError("Any player is not exist in game!");
        //}
    }

    /// <summary>
    /// 이새끼가 씬 바꾸는 녀석임 코루틴 실행 실킬겨
    /// </summary>
    /// <param name="sceneType"></param>
    public void SceneChange(SceneType sceneType)
    {
        StartCoroutine(SceneChangeCor(sceneType));
    }

    private IEnumerator SceneChangeCor(SceneType sceneType)
    {
        AsyncOperation async =  SceneManager.LoadSceneAsync(sceneType.ToString());
        yield return new WaitUntil(() => async.isDone);
        UIManager.Instanace.HandleUIChange(sceneType);
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
