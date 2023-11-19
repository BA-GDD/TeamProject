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

    [SerializeField] private List<GameObject> _sceneUIList = new List<GameObject>();

    private OptionPanel _op;

    public void UIChange(SceneType toChangeScene)
    {
        if (UIManager.Instanace.currentSceneObject != null)
            Destroy(UIManager.Instanace.currentSceneObject);

        if (_canvasTrm == null)
            _canvasTrm = GameObject.Find("UICanvas").transform;

        UIManager.Instanace.currentSceneObject = Instantiate(_sceneUIList[(int)toChangeScene], _canvasTrm);
        UIManager.Instanace.currentSceneType = toChangeScene;
        _op = Instantiate(_optionPanel, _canvasTrm);
        _op.gameObject.name = "OptionPanel";
        _op.gameObject.SetActive(false);
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
        ResultUI ru = Instantiate(_resultPanel, _canvasTrm);
        ru.transform.localPosition = _resultPanelCreatePos;
        ru.gameObject.name = "Result";
        ru.ActiveResultPanel(ScoreManager.Instance.Score, combo, clearTime, dealDamage);
    }
    [ContextMenu("�ɼ� �г� Ȱ��ȭ")]
    public void ActiveOptionPanel(bool isOpen)
    {
        if(isOpen == false)
        {
            _op.OpenPanel();
        }
        else
        {
            _op.ClosePanel();
        }
        
    }
    [ContextMenu("���� ���� �г� Ȱ��ȭ")]
    public void ActiveGameOverPanel()
    {
        Instantiate(_gameOverPanel, _canvasTrm);
    }
    #endregion
}


