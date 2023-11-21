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
    public Action<string> HandleActiveWarnningPanel;
    #endregion

    #region ?¥ê? ?????? ????? UI ????
    public Action<DifficultyType, int, bool> HandleStarPut; // ????? ????? ?? ?? ???? ??????? ????? ??? ?¥ê?? ??????? ??
                                                            // bool = true, ???? bool = false;

    public Action<MapInfo> HandleClickPlayPanel; // ?¡À??? ?¬Ô??? ?????? ?? ?????? ????.
                                                 // ?????? MapInfo ??????? ??????.
                                                 // MapInfo ??????? ?¡À??? ????? ??????.
    #endregion

    #region ???????? ????? UI ????
    public Action HandleInGameStartEvent;
    public Action<float> HandleUseSkill; // ??? ??? float = cooltime
    public Action<float> HandlePlayerGetDamage; // ?¡À???? ?????? ????
    public Action<float> HandleBossGetDamage; // ???? ?????? ????
    public Action HandlePlusCombo; // ??? ?¡À???
    public Action HandleResetCombo; // ??? ????
    public Action HandleShootGun; // UI ?? ???
    public Action HandleReload; // UI ?? ????
    public Action<int, float, float> HandleGameClear; // ??? ???, ????? ?©£?, ???? ??????
    public Action HandleGameOver; // ???? ???? ?¬Ô? ????
    public Action HandleRetryGame; // ???? ?????
    #endregion

    [Header("??????? ????")]
    [SerializeField] private float _spectrumNormalValue;
    public float bgm_SpectrumSizeValue;
    public float sfx_SpectrumSizeValue;

    private bool _optionPanelOpen = false;
    private bool _isOnUI = false;

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
        _isOnUI = true;
    }

    public void TurnOffAllUI()
    {
        _isOnUI = false;
        UIHud.TurnOff();
    }
    public void TurnOnAllUI()
    {
        _isOnUI = true; 
        UIHud.TurnOn();
    }

    private void Start()
    {
        HandleActiveOptionPanel += SetOptionPanel;
        HandleUIChange += UIHud.UIChange;
        HandleGameExit += UIHud.ActiveGameExitPanel;
        HandleGameOver += UIHud.ActiveGameOverPanel;
        HandleActiveWarnningPanel += UIHud.ActiveWarnningPanel;
        //HandleRetryGame += GameManager.instance.GameRestart;

        HandleUIChange?.Invoke(currentSceneType);
    }
    public void SceneChange()
    {
        UIHud.SceneChange();
    }
    private void SetOptionPanel()
    {
        if (_isOnUI == false) return;
        UIHud.ActiveOptionPanel(_optionPanelOpen);
        if (currentSceneType != SceneType.title)
        {
            _inputReader.OpenSetting(_optionPanelOpen);
            RhythmManager.instance.GameStop(_optionPanelOpen);
        }
        if(_optionPanelOpen)
        {
            Cursor.lockState = CursorLockMode.Confined;
            if(currentSceneType == SceneType.inGame)
                Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
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