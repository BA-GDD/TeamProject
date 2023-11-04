using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RhythmAction
{
    Shoot,
    Reload,
    Dash,
}

public class RhythmManager : MonoBehaviour
{
    public static RhythmManager instance = null;
    [SerializeField]
    private AudioClip _metronomeClip;
    [SerializeField]
    private AudioSource _musicAudioSource;
    [SerializeField]
    private AudioSource _metronomeAudioSource;
    [SerializeField]
    private MusicDataSO _musicDataSO;
    [SerializeField]
    private bool _metronomeMode = false;
    [SerializeField]
    private float _judgementRangeFrame;
    [SerializeField]
    private float _judgementOffsetFrame;
    private int _comboCount;
    public int ComboCount => _comboCount;
    private float _durationPerTime;
    private float _samplePerTime;
    private float _judgementRangeSample;
    private float _judgementOffsetSample;
    private float _judgementPointSample;
    private float _nextSample;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("RhythmManager instance is already exist!");
        }

        instance = this;
        _musicAudioSource.clip = _musicDataSO.music;
    }

    private void Start()
    {
        _durationPerTime = 60f / _musicDataSO.beatPerMinute * (_musicDataSO.timeNumerator / _musicDataSO.timeDenominator);
        _samplePerTime = _durationPerTime * _musicAudioSource.clip.frequency;
        _judgementRangeSample = _musicAudioSource.clip.frequency * (_judgementRangeFrame / 60f);
        _judgementOffsetSample = _musicAudioSource.clip.frequency * (_judgementOffsetFrame / 60f);
        _nextSample = _musicAudioSource.clip.frequency * _musicDataSO.offset;

        StartCoroutine(PlayMusic());
    }

    private void Update()
    {
        if (_metronomeMode && _musicAudioSource.timeSamples >= _nextSample)
        {
            StartCoroutine(PlayMetronome());
        }
    }

    /// <summary>
    /// 판정 여부 리턴: 정해진 박자에서 설정된 프레임 오차 내 호출 시 true, 아니면 false
    /// </summary>
    public bool Judgement(RhythmAction action, bool canPlusCombo = false)
    {
        _judgementPointSample = _musicAudioSource.timeSamples - _judgementOffsetSample;
        bool isOnTiming = Mathf.Min(-(_nextSample - _samplePerTime - _judgementPointSample), -(_judgementPointSample - _nextSample)) <= _judgementRangeSample;

        if (action == RhythmAction.Shoot && isOnTiming && canPlusCombo)
        {
            _comboCount++;
        }
        else
        {
            _comboCount = 0;
        }

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

        _musicAudioSource.Play();
    }
}
