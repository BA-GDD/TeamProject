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

    [Header("�� UI")]
    [SerializeField] private List<GameObject> _sceneUIList = new List<GameObject>();

    public void UIChange(UISceneType toChangeScene)
    {
        if(UIManager.Instanace.currentSceneObject != null)
            Destroy(UIManager.Instanace.currentSceneObject);

        UIManager.Instanace.currentSceneObject = Instantiate(_sceneUIList[(int)toChangeScene], _canvasTrm);
        UIManager.Instanace.currentSceneType = toChangeScene;
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
    #endregion
}


