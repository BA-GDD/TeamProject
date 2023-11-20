using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instanace
    {
        get
        {
            if (_instance != null) return _instance;
            _instance = FindObjectOfType<UIManager>();
            if (_instance == null)
            {
                Debug.LogError("Not Exist UIManager");
            }
            return _instance;
        }
    }
    public UIHud UIHud;
    public SceneType currentSceneType;
    public GameObject currentSceneObject;
    public float rhythmTurm;

    #region ????? ??? ?? ??? UI ????
    public Action<SceneType> HandleUIChange; // ?? ??? ?? ???? ?? ????
    public Action HandleActiveOptionPanel; // ???? ?? ???? ????
    public Action HandleGameExit; // ???? ???? ????
    #endregion

    #region ?��? ?????? ????? UI ????
    public Action<DifficultyType, int, bool> HandleStarPut; // ????? ????? ?? ?? ???? ??????? ????? ??? ?��?? ??????? ??
                                                            // bool = true, ???? bool = false;

    public Action<MapInfo> HandleClickPlayPanel; // ?��??? ?��??? ?????? ?? ?????? ????.
                                                 // ?????? MapInfo ??????? ??????.
                                                 // MapInfo ??????? ?��??? ????? ??????.
    #endregion

    #region ???????? ????? UI ????
    public Action HandleInGameStartEvent;
    public Action<float> HandleUseSkill; // ??? ??? float = cooltime
    public Action<float> HandlePlayerGetDamage; // ?��???? ?????? ????
    public Action<float> HandleBossGetDamage; // ???? ?????? ????
    public Action HandlePlusCombo; // ??? ?��???
    public Action HandleResetCombo; // ??? ????
    public Action HandleShootGun; // UI ?? ???
    public Action HandleReload; // UI ?? ????
    public Action<int, float, float> HandleGameClear; // ??? ???, ????? ?��?, ???? ??????
    public Action HandleGameOver; // ???? ???? ?��? ????
    public Action HandleRetryGame; // ???? ?????
    #endregion

    [Header("??????? ????")]
    [SerializeField] private float _spectrumNormalValue;
    public float bgm_SpectrumSizeValue;
    public float sfx_SpectrumSizeValue;

    private bool _optionPanelOpen = false;

    [SerializeField] private InputReader _inputReader;

    private void Awake()
    {
        if (_instance != null)
        {
            Debug.LogError($"{typeof(UIManager)} instance is already exist!");
            Destroy(gameObject);
            return;
        }

        UIHud = (UIHud)transform.Find("UIHud").GetComponent("UIHud");
        bgm_SpectrumSizeValue = sfx_SpectrumSizeValue = _spectrumNormalValue;
        _optionPanelOpen = false;
    }

    public void TurnOffAllUI()
    {
        UIHud.TurnOff();
    }
    public void TurnOnAllUI()
    {
        UIHud.TurnOn();
    }

    private void Start()
    {
        HandleActiveOptionPanel += SetOptionPanel;
        HandleUIChange += UIHud.UIChange;
        HandleGameExit += UIHud.ActiveGameExitPanel;
        HandleGameOver += UIHud.ActiveGameOverPanel;
        //HandleRetryGame += GameManager.instance.GameRestart;

        //HandleUIChange?.Invoke(currentSceneType);
    }
    public void SceneChange()
    {
        UIHud.SceneChange();
    }
    private void SetOptionPanel()
    {
        UIHud.ActiveOptionPanel(_optionPanelOpen);
        if (currentSceneType != SceneType.title)
        {
            _inputReader.OpenSetting(_optionPanelOpen);
            RhythmManager.instance.GameStop(_optionPanelOpen);
        }
        _optionPanelOpen = !_optionPanelOpen;
    }

    public void SetSpectrumValue(SoundType st, float value)
    {
        switch (st)
        {
            case SoundType.BGM:
                bgm_SpectrumSizeValue = _spectrumNormalValue * value;
                break;
            case SoundType.SFX:
                sfx_SpectrumSizeValue = _spectrumNormalValue * value;
                break;
            default:
                break;
        }
    }
}