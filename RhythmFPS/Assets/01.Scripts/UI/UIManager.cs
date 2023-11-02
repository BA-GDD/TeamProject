using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BarType
{
    playerHp,
    enemyHp,
    bossHp,
    bulletCount
}

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("Warnning!! UIManager Has Multiply Running!");
            return;
        }
        Instance = this;
    }

    private void Start()
    {

    }

}
