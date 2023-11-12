using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyBtn : GameOverBtn
{
    public override void ClickLogic()
    {
        UIManager.Instanace.HandleUIChange?.Invoke(UISceneType.lobby);
        Destroy(gameOverUI);
    }
}
