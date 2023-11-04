using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BarType
{
    playerHp,
    enemyHp,
    bossHp,
    bulletCount
}

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [Header("ĵ���� Ʈ������")]
    [SerializeField] private Transform _canvasTrm;

    [Header("��� �г�")]
    [SerializeField] private Vector3 _resultPanelCreatePos;
    [SerializeField] ResultUI _resultPanel;

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("Warnning!! UIManager Has Multiply Running!");
            return;
        }
        Instance = this;
    }

    [ContextMenu("����Ʈ �г� Ȱ��ȭ")]
    public void ActiveResultPanel()
    {
        ResultUI ru = Instantiate(_resultPanel, _canvasTrm);
        ru.transform.localPosition = _resultPanelCreatePos;
        ru.gameObject.name = "Result";
        ru.ActiveResultPanel(1, 1, 1, 1);
    }
}
