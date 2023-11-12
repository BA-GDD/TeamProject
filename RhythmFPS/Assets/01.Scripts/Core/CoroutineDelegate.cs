using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineDelegate : MonoBehaviour
{
    private static CoroutineDelegate instance;
    public static CoroutineDelegate Instance
    {
        get 
        {
            if(instance == null)
            {
                instance = FindObjectOfType<CoroutineDelegate>();
            }
            if(instance == null)
            {
                Debug.LogWarning($"Warning! {typeof(CoroutineDelegate)} is not have this scene");
                return null;
            }
            return instance;
        }
    }


    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    public Coroutine StartCoroutineHandle(IEnumerator cor)
    {
        return StartCoroutine(cor);
    }
    public void StopCoroutineHandle(Coroutine cor)
    {
        StopCoroutine(cor);
    }

}
