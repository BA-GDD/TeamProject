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

    #region ��𼭵� �Ͼ �� �ִ� UI �̺�Ʈ
    public Action<SceneType> HandleUIChange; // �� �ٲ� �� ���� �� �̺�Ʈ
    public Action HandleActiveOptionPanel; // ���� �� Ȱ��ȭ �̺�Ʈ
    public Action HandleGameExit; // ���� ���� �̺�Ʈ
    #endregion

    #region �κ� ������ �Ͼ�� UI �̺�Ʈ
    public Action<DifficultyType, int, bool> HandleStarPut; // �ѹ��� Ŭ���� �� �� ���� ���ǵ��� Ŭ���� �ϰ� �κ�� ���ƿ��� ��
                                                            // bool = true, �ƴϸ� bool = false;

    public Action<MapInfo> HandleClickPlayPanel; // �÷��� �г��� ������ �� ����Ǵ� �̺�Ʈ.
                                                 // �ش��ϴ� MapInfo Ŭ������ �Ѱ��ش�.
                                                 // MapInfo Ŭ������ �÷��� ����� ����ȴ�.
    #endregion

    #region �ΰ��ӿ��� �Ͼ�� UI �̺�Ʈ
    public Action HandleInGameStartEvent;
    public Action<float> HandleUseSkill; // ��ų ��� float = cooltime
    public Action<float> HandlePlayerGetDamage; // �÷��̾� ������ ����
    public Action<float> HandleBossGetDamage; // ���� ������ ����
    public Action HandlePlusCombo; // �޺� �÷���
    public Action HandleResetCombo; // �޺� ����
    public Action HandleShootGun; // UI źâ �Ҹ�
    public Action HandleReload; // UI źâ ����
    public Action<int, float, float> HandleGameClear; // �޼� �޺�, Ŭ���� �ð�, ���� ������
    public Action HandleGameOver; // ���� ���� �г� Ȱ��ȭ
    public Action HandleRetryGame; // ���� �����
    #endregion

    [Header("����Ʈ�� ����")]
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

    private void Start()
    {
        HandleActiveOptionPanel += SetOptionPanel;
        HandleUIChange += UIHud.UIChange;
        HandleGameExit += UIHud.ActiveGameExitPanel;
        HandleGameOver += UIHud.ActiveGameOverPanel;
        //HandleRetryGame += GameManager.instance.GameRestart;

        HandleUIChange?.Invoke(currentSceneType);
    }
    public void SceneChange()
    {
        UIHud.SceneChange();
    }
    private void SetOptionPanel()
    {
        UIHud.ActiveOptionPanel(_optionPanelOpen);
        if(currentSceneType != SceneType.lobby)
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
