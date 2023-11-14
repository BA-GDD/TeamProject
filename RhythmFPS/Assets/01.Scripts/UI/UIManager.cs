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

    #region 어디서든 일어날 수 있는 UI 이벤트
    public Action<UISceneType> HandleUIChange; // 씬 바뀔 때 발행 할 이벤트
    public Action HandleActiveOptionPanel; // 설정 찰 활성화 이벤트
    public Action HandleGameExit; // 게임 종료 이벤트
    #endregion

    #region 로비 씬에서 일어나는 UI 이벤트
    public Action<DifficultyType, int, bool> HandleStarPut; // 한번도 클리어 한 적 없는 난의도를 클리어 하고 로비로 돌아왔을 때
                                                            // bool = true, 아니면 bool = false;

    public Action<MapInfo> HandleClickPlayPanel; // 플레이 패널을 눌렀을 때 발행되는 이벤트.
                                                 // 해당하는 MapInfo 클래스를 넘겨준다.
                                                 // MapInfo 클래스는 플레이 기록이 저장된다.
    #endregion

    #region 인게임에서 일어나는 UI 이벤트
    public Action HandleInGameStartEvent;
    public Action<float> HandleUseSkill; // 스킬 사용 float = cooltime
    public Action<float> HandlePlayerGetDamage; // 플레이어 데미지 입음
    public Action<float> HandleBossGetDamage; // 보스 데미지 입음
    public Action HandlePlusCombo; // 콤보 플러스
    public Action HandleResetCombo; // 콤보 리셋
    public Action HandleShootGun; // UI 탄창 소모
    public Action HandleReload; // UI 탄창 장전
    public Action<int, float, float> HandleGameClear; // 달성 콤보, 클리어 시간, 넣은 데미지
    public Action HandleGameOver; // 게임 오버 패널 활성화
    public Action HandleRetryGame; // 게임 재시작
    #endregion

    [Header("스펙트럼 조정")]
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
