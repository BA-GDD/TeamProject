using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSingleton : MonoBehaviour
{
    private static BossSingleton _instance;
    public static BossSingleton Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<BossSingleton>();
            }
            if (_instance == null)
            {
                Debug.LogWarning($"Warning! {typeof(BossSingleton)} is not have this scene");
                return null;
            }
            return _instance;
        }
    }
    [SerializeField]
    private BossBrain _boss;
    public BossBrain Boss => _boss;
}
