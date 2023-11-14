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
    public UISceneType currentSceneType;
    public GameObject currentSceneObject;
    public float rhythmTurm;

    #region ��𼭵� �Ͼ �� �ִ� UI �̺�Ʈ
    public Action<UISceneType> HandleUIChange; // �� �ٲ� �� ���� �� �̺�Ʈ
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

    private void Awake()
    {
        UIHud = (UIHud)transform.Find("UIHud").GetComponent("UIHud");
        bgm_SpectrumSizeValue = sfx_SpectrumSizeValue = _spectrumNormalValue;
    }

    private void Start()
    {
        HandleUIChange += UIHud.UIChange;
        HandleActiveOptionPanel += UIHud.ActiveOptionPanel;
        HandleGameExit += UIHud.ActiveGameExitPanel;
        HandleGameOver += UIHud.ActiveGameOverPanel;
        //HandleRetryGame += GameManager.instance.GameRestart;

        HandleUIChange?.Invoke(currentSceneType);
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
}
