using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyBtn : GameOverBtn
{
    public override void ClickLogic()
    {
        GameManager.instance.SceneChange(SceneType.lobby);
        //UIManager.Instanace.HandleUIChange?.Invoke(SceneType.lobby);
        Destroy(gameOverUI);
    }
}
