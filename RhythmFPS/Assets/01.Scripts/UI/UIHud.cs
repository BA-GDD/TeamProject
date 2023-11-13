using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UIHud : MonoBehaviour
{
    [Header("캔버스 트랜스폼")]
    [SerializeField] private Transform _canvasTrm;

    [Header("결과 패널")]
    [SerializeField] private Vector3 _resultPanelCreatePos;
    [SerializeField] private ResultUI _resultPanel;

    [Header("옵션 패널")]
    [SerializeField] private OptionPanel _optionPanel;

    [Header("나가기 UI")]
    [SerializeField] private ExitPanel _exitPanel;

    [Header("게임 오버 패널")]
    [SerializeField] private GameObject _gameOverPanel;

    [Header("씬 UI")]
    [SerializeField] private List<GameObject> _sceneUIList = new List<GameObject>();

    public void UIChange(UISceneType toChangeScene)
    {
        if(UIManager.Instanace.currentSceneObject != null)
            Destroy(UIManager.Instanace.currentSceneObject);

        UIManager.Instanace.currentSceneObject = Instantiate(_sceneUIList[(int)toChangeScene], _canvasTrm);
        UIManager.Instanace.currentSceneType = toChangeScene;
    }

    private void Update()
    {
        /*if(Input.GetKeyDown(KeyCode.Space))
        {
            ActiveResultPanel(1, 1, 1);
        }*/
    }

    #region 패널 활성화
    [ContextMenu("나가기 패널 활성화")]
    public void ActiveGameExitPanel()
    {
        Instantiate(_exitPanel, _canvasTrm).SetupPanel(UIManager.Instanace.currentSceneType);
    }
    [ContextMenu("리절트 패널 활성화")]
    public void ActiveResultPanel(int combo, float clearTime, float dealDamage)
    {
        float score = clearTime * combo + dealDamage;

        ResultUI ru = Instantiate(_resultPanel, _canvasTrm);
        ru.transform.localPosition = _resultPanelCreatePos;
        ru.gameObject.name = "Result";
        ru.ActiveResultPanel(score, combo, clearTime, dealDamage);
    }
    [ContextMenu("옵션 패널 활성화")]
    public void ActiveOptionPanel()
    {
        OptionPanel op = Instantiate(_optionPanel, _canvasTrm);
        op.transform.localPosition = Vector3.zero;
        op.gameObject.name = "OptionPanel";
        op.OpenPanel();
    }
    [ContextMenu("게임 오버 패널 활성화")]
    public void ActiveGameOverPanel()
    {
        Instantiate(_gameOverPanel, _canvasTrm);
    }
    #endregion
}


