using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RhythmAction
{
    Shoot,
    Reload,
    Ability,
}

public class RhythmManager : MonoBehaviour
{
    public static RhythmManager instance = null;
    [Tooltip("��Ʈ�� �ִ� ���ڿ� �� �̺�Ʈ�� ȣ��˴ϴ�.")]
    public Action onNotedTimeAction;
    [SerializeField]
    private AudioClip _metronomeClip;
    [SerializeField]
    private AudioSource _musicAudioSource;
    public AudioSource musicAudioSource => _musicAudioSource;
    [SerializeField]
    private AudioSource _metronomeAudioSource;
    [SerializeField]
    private MusicDataSO _musicDataSO;
    [SerializeField, Tooltip("���� �� ������ true��� ��Ʈ�� �ִ� ���ڿ� ��Ʈ�γ� Ŭ���� ����˴ϴ�.")]
    private bool _metronomeMode = false;
    [SerializeField, Tooltip("���� ���� �Լ��� ��Ʈ�� �ִ� ���ڷκ��� +- ���� ������ ���� ȣ���ϸ� true��, �ƴϸ� false�� ��ȯ�մϴ�.")]
    private float _judgementRangeFrame;
    [SerializeField, Tooltip("���� ���� �Լ��� ȣ���ϸ�, ������ �����Ӹ�ŭ ������ ����ؼ� �����˴ϴ�.")]
    private float _judgementOffsetFrame;
    private float _durationPerTime;
    private float _samplePerTime;
    private float _judgementRangeSample;
    private float _judgementOffsetSample;
    private float _judgementPointSample;
    private float _nextSample;
    private float _judgementNextSample;

    private bool _isCanActive;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("RhythmManager instance is already exist!");
        }

        instance = this;
        Debug.Log(instance);
        _musicAudioSource.clip = _musicDataSO.music;
        _isCanActive = true;
    }

    private void Start()
    {
        _durationPerTime = 60f / _musicDataSO.beatPerMinute * (_musicDataSO.timeNumerator / _musicDataSO.timeDenominator);
        _samplePerTime = _durationPerTime * _musicAudioSource.clip.frequency;
        _judgementRangeSample = _musicAudioSource.clip.frequency * (_judgementRangeFrame / 60f);
        _judgementOffsetSample = _musicAudioSource.clip.frequency * (_judgementOffsetFrame / 60f);
        _nextSample = _musicAudioSource.clip.frequency * _musicDataSO.offset;
        _judgementNextSample = _nextSample + _judgementRangeSample;

        StartCoroutine(PlayMusic());
    }

    private void Update()
    {
        if (_metronomeMode && _musicAudioSource.timeSamples >= _nextSample)
        {
            onNotedTimeAction?.Invoke();
            StartCoroutine(PlayMetronome());
        }

        if (_musicAudioSource.timeSamples >= _judgementNextSample)
        {
            _judgementNextSample = _nextSample + _judgementRangeSample;
            _isCanActive = true;
        }
    }

    /// <summary>
    /// ���� ���� ����: ������ ���ڿ��� ������ ������ ���� �� ȣ�� �� true, �ƴϸ� false
    /// </summary>
    public bool Judgement()
    {
        if (_isCanActive == false) return false;
        _judgementPointSample = _musicAudioSource.timeSamples - _judgementOffsetSample;
        bool isOnTiming = Mathf.Min(-(_nextSample - _samplePerTime - _judgementPointSample), -(_judgementPointSample - _nextSample)) <= _judgementRangeSample;

        if (isOnTiming == false) ComboManager.Instance.ResetCombo();
        if (isOnTiming == true) _isCanActive = false;
        return isOnTiming;
    }

    private IEnumerator PlayMetronome()
    {
        _nextSample += _samplePerTime;

        _metronomeAudioSource.PlayOneShot(_metronomeClip);


        yield return null;
    }

    private IEnumerator PlayMusic()
    {
        yield return null;
        UIManager.Instanace.HandleInGameStartEvent?.Invoke();
        _musicAudioSource.Play();
    }
    public void GameStop(bool value)
    {
        if(value == false)
        {
            _musicAudioSource.Pause();
        }
        else
        {
            _musicAudioSource.Play();
        }
    }    
    
}
