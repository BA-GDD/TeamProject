using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHud : MonoBehaviour
{
    [Header("캔버스 트랜스폼")]
    [SerializeField] private Transform _canvasTrm;

    [Header("결과 패널")]
    [SerializeField] private Vector3 _resultPanelCreatePos;
    [SerializeField] private ResultUI _resultPanel;

    [Header("옵션 패널")]
    [SerializeField] private OptionPanel _optionPanel;

    [Header("게임 스타트 패널")]
    [SerializeField] private Transform _startScene;

    [Header("로비 UI")]
    [SerializeField] private Transform _lobbyUI;

    [ContextMenu("리절트 패널 활성화")]
    public void ActiveResultPanel(/* 매개변수 받기 */)
    {
        ResultUI ru = Instantiate(_resultPanel, _canvasTrm);
        ru.transform.localPosition = _resultPanelCreatePos;
        ru.gameObject.name = "Result";
        ru.ActiveResultPanel(1, 1, 1, 1 /*매개변수 설정*/);
    }

    [ContextMenu("옵션 패널 활성화")]
    public void ActiveOptionPanel()
    {
        OptionPanel op = Instantiate(_optionPanel, _canvasTrm);
        op.transform.localPosition = Vector3.zero;
        op.gameObject.name = "OptionPanel";
        op.OpenPanel();
    }

    [ContextMenu("스타트 패널 활성화")]
    public void ActiveStartScene()
    {
        Instantiate(_startScene, _canvasTrm);
    }

    [ContextMenu("로비 UI 활성화")]
    public void ActiveLobbyUI()
    {
        Instantiate(_lobbyUI, _canvasTrm);
    }
}
