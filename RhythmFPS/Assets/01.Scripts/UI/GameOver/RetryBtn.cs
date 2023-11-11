using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetryBtn : GameOverBtn
{
    public override void ClickLogic()
    {
        UIManager.Instanace.HandleRetryGame?.Invoke();
        Destroy(gameOverUI);
    }
}
