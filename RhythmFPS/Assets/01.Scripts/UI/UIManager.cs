using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [Header("ĵ���� Ʈ������")]
    [SerializeField] private Transform _canvasTrm;

    [Header("��� �г�")]
    [SerializeField] private Vector3 _resultPanelCreatePos;
    [SerializeField] private ResultUI _resultPanel;

    [Header("�ɼ� �г�")]
    [SerializeField] private OptionPanel _optionPanel;

    [Header("����Ʈ�� ����")]
    [SerializeField] private float _spectrumNormalValue;
    public float bgm_SpectrumSizeValue;
    public float sfx_SpectrumSizeValue;

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("Warnning!! UIManager Has Multiply Running!");
            return;
        }
        Instance = this;
    }

    public void SetSpectrumValue(SoundType st, float value)
    {
        switch (st)
        {
            case SoundType.bgm:
                bgm_SpectrumSizeValue = _spectrumNormalValue * value;
                break;
            case SoundType.sfx:
                sfx_SpectrumSizeValue = _spectrumNormalValue * value;
                break;
            default:
                break;
        }
    }

    [ContextMenu("����Ʈ �г� Ȱ��ȭ")]
    public void ActiveResultPanel(/* �Ű����� �ޱ� */)
    {
        ResultUI ru = Instantiate(_resultPanel, _canvasTrm);
        ru.transform.localPosition = _resultPanelCreatePos;
        ru.gameObject.name = "Result";
        ru.ActiveResultPanel(1, 1, 1, 1 /*�Ű����� ����*/);
    }

    // �ɼ� �����°� �����
    [ContextMenu("�ɼ� �г� Ȱ��ȭ")]
    public void ActiveOptionPanel()
    {
        OptionPanel op = Instantiate(_optionPanel, _canvasTrm);
        op.transform.localPosition = Vector3.zero;
        op.gameObject.name = "OptionPanel";
        op.OpenPanel();
    }
}
