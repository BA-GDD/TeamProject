using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHud : MonoBehaviour
{
    [Header("ĵ���� Ʈ������")]
    [SerializeField] private Transform _canvasTrm;

    [Header("��� �г�")]
    [SerializeField] private Vector3 _resultPanelCreatePos;
    [SerializeField] private ResultUI _resultPanel;

    [Header("�ɼ� �г�")]
    [SerializeField] private OptionPanel _optionPanel;

    [Header("���� ��ŸƮ �г�")]
    [SerializeField] private Transform _startScene;

    [Header("�κ� UI")]
    [SerializeField] private Transform _lobbyUI;

    [ContextMenu("����Ʈ �г� Ȱ��ȭ")]
    public void ActiveResultPanel(/* �Ű����� �ޱ� */)
    {
        ResultUI ru = Instantiate(_resultPanel, _canvasTrm);
        ru.transform.localPosition = _resultPanelCreatePos;
        ru.gameObject.name = "Result";
        ru.ActiveResultPanel(1, 1, 1, 1 /*�Ű����� ����*/);
    }

    [ContextMenu("�ɼ� �г� Ȱ��ȭ")]
    public void ActiveOptionPanel()
    {
        OptionPanel op = Instantiate(_optionPanel, _canvasTrm);
        op.transform.localPosition = Vector3.zero;
        op.gameObject.name = "OptionPanel";
        op.OpenPanel();
    }

    [ContextMenu("��ŸƮ �г� Ȱ��ȭ")]
    public void ActiveStartScene()
    {
        Instantiate(_startScene, _canvasTrm);
    }

    [ContextMenu("�κ� UI Ȱ��ȭ")]
    public void ActiveLobbyUI()
    {
        Instantiate(_lobbyUI, _canvasTrm);
    }
}
