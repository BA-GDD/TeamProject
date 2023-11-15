using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UIHud : MonoBehaviour
{
    [Header("ĵ���� Ʈ������")]
    [SerializeField] private Transform _canvasTrm;

    [Header("��� �г�")]
    [SerializeField] private Vector3 _resultPanelCreatePos;
    [SerializeField] private ResultUI _resultPanel;

    [Header("�ɼ� �г�")]
    [SerializeField] private OptionPanel _optionPanel;

    [Header("������ UI")]
    [SerializeField] private ExitPanel _exitPanel;

    [Header("���� ���� �г�")]
    [SerializeField] private GameObject _gameOverPanel;

    [Header("씬 체인지 UI")]
    [SerializeField] private SceneChangeUI _sceneChangeUI;

    public void UIChange(SceneType toChangeScene)
    {
        Instantiate(_sceneChangeUI, _canvasTrm).StartChange(toChangeScene);
    }

    private void Update()
    {
        /*if(Input.GetKeyDown(KeyCode.Space))
        {
            ActiveResultPanel(1, 1, 1);
        }*/
    }
    #region �г� Ȱ��ȭ
    [ContextMenu("������ �г� Ȱ��ȭ")]
    public void ActiveGameExitPanel()
    {
        Instantiate(_exitPanel, _canvasTrm).SetupPanel(UIManager.Instanace.currentSceneType);
    }
    [ContextMenu("����Ʈ �г� Ȱ��ȭ")]
    public void ActiveResultPanel(int combo, float clearTime, float dealDamage)
    {
        float score = clearTime * combo + dealDamage;

        ResultUI ru = Instantiate(_resultPanel, _canvasTrm);
        ru.transform.localPosition = _resultPanelCreatePos;
        ru.gameObject.name = "Result";
        ru.ActiveResultPanel(score, combo, clearTime, dealDamage);
    }
    [ContextMenu("�ɼ� �г� Ȱ��ȭ")]
    public void ActiveOptionPanel()
    {
        OptionPanel op = Instantiate(_optionPanel, _canvasTrm);
        op.transform.localPosition = Vector3.zero;
        op.gameObject.name = "OptionPanel";
        op.OpenPanel();
    }
    [ContextMenu("���� ���� �г� Ȱ��ȭ")]
    public void ActiveGameOverPanel()
    {
        Instantiate(_gameOverPanel, _canvasTrm);
    }
    #endregion
}


