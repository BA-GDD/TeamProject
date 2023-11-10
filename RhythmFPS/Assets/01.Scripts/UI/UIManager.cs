using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instnace
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

    #region ��𼭵� �Ͼ �� �ִ� UI �̺�Ʈ
    public Action<SceneType> HandleSceneChange; // �� �ٲ� �� ���� �� �̺�Ʈ
    public Action HandleActiveOptionPanel; // ���� �� Ȱ��ȭ �̺�Ʈ
    public Action handleGameExit; // ���� ���� �̺�Ʈ
    #endregion

    #region �κ� ������ �Ͼ�� UI �̺�Ʈ
    public Action<int, bool> HandleStarPut; // �ѹ��� Ŭ���� �� �� ���� ���� Ŭ���� �ϰ� �κ�� ���ƿ��� ��
                                            // bool = true, �ƴϸ� bool = false;

    public Action<MapInfo> HandleClickPlayPanel; // �÷��� �г��� ������ �� ����Ǵ� �̺�Ʈ.
                                                       // �ش��ϴ� MapInfo Ŭ������ �Ѱ��ش�.
                                                       // MapInfo Ŭ������ �÷��� ����� ����ȴ�.
    #endregion

    #region �ΰ��ӿ��� �Ͼ�� UI �̺�Ʈ
    public Action HandleUseSkill; // ��ų ���
    public Action<float> HandlePlayerGetDamage; // �÷��̾� ������ ����
    public Action<float> HandleBossGetDamage; // ���� ������ ����
    public Action HandlePlusCombo; // �޺� �÷���
    public Action HandleShootGun; // UI źâ �Ҹ�
    public Action HandleReload; // UI źâ ����
    public Action<int, float, float> HandleGameClear; // �޼� �޺�, Ŭ���� �ð�, ���� ������
    #endregion

    [Header("����Ʈ�� ����")]
    [SerializeField] private float _spectrumNormalValue;
    public float bgm_SpectrumSizeValue;
    public float sfx_SpectrumSizeValue;

    private void Awake()
    {
        bgm_SpectrumSizeValue = sfx_SpectrumSizeValue = _spectrumNormalValue;
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
