using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class ExitPanel : MonoBehaviour
{
    [SerializeField] private Transform _panel;
    [SerializeField] private TextMeshProUGUI _synteText;
    [SerializeField] private float _easingTime = 0.4f;
    [SerializeField] private Button _acceptBtn;

    public void SetupPanel(UISceneType sceneT)
    {
        _panel.DOLocalMoveY(0, _easingTime);

        if(sceneT == UISceneType.inGame)
        {
            _synteText.text = "로비로 퇴장하시겠습니까?\n현재 진행 상황이 저장되지 않습니다.";
            _acceptBtn.onClick.AddListener(ToLobby);
        }
        else
        {
            _synteText.text = "게임을 종료 하시겠습니까?";
            _acceptBtn.onClick.AddListener(Application.Quit);
        }
    }

    private void ToLobby()
    {
        UIManager.Instanace.HandleUIChange?.Invoke(UISceneType.lobby);
        Destroy(_panel.parent.gameObject);
    }

    public void Cancle()
    {
        _panel.DOLocalMoveY(-800, _easingTime).SetEase(Ease.InBack);
    }

    private void OnDestroy()
    {
        _acceptBtn.onClick.RemoveAllListeners();
    }
}
