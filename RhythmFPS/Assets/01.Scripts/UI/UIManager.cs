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
    [SerializeField] ResultUI _resultPanel;

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
    public void ActiveResultPanel()
    {
        ResultUI ru = Instantiate(_resultPanel, _canvasTrm);
        ru.transform.localPosition = _resultPanelCreatePos;
        ru.gameObject.name = "Result";
        ru.ActiveResultPanel(1, 1, 1, 1);
    }
}
