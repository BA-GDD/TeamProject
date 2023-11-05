using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [Header("캔버스 트랜스폼")]
    [SerializeField] private Transform _canvasTrm;

    [Header("결과 패널")]
    [SerializeField] private Vector3 _resultPanelCreatePos;
    [SerializeField] private ResultUI _resultPanel;

    [Header("옵션 패널")]
    [SerializeField] private OptionPanel _optionPanel;

    [Header("스펙트럼 조정")]
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

    [ContextMenu("리절트 패널 활성화")]
    public void ActiveResultPanel(/* 매개변수 받기 */)
    {
        ResultUI ru = Instantiate(_resultPanel, _canvasTrm);
        ru.transform.localPosition = _resultPanelCreatePos;
        ru.gameObject.name = "Result";
        ru.ActiveResultPanel(1, 1, 1, 1 /*매개변수 설정*/);
    }

    // 옵션 나오는거 만드셈
    [ContextMenu("옵션 패널 활성화")]
    public void ActiveOptionPanel()
    {
        OptionPanel op = Instantiate(_optionPanel, _canvasTrm);
        op.transform.localPosition = Vector3.zero;
        op.gameObject.name = "OptionPanel";
        op.OpenPanel();
    }
}
