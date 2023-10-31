using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metronome : MonoBehaviour
{
    [SerializeField]
    private AudioClip metronomeClip;
    [SerializeField]
    private AudioSource musicAudioSource;
    [SerializeField]
    private AudioSource metronomeAudioSource;
    [SerializeField]
    private MusicDataSO musicDataSO;
    [SerializeField]
    private bool isTestMode = false;
    [SerializeField]
    private float judgementRangeFrame;
    [SerializeField]
    private float judgementOffsetFrame;
    private float durationPerTime;
    private float samplePerTime;
    private float judgementRangeSample;
    private float judgementOffsetSample;
    private float judgementPointSample;
    private float nextSample;

    private bool isCheckCurBeat;

    public float percent { get => (nextSample - musicAudioSource.timeSamples) / samplePerTime; }

    private void Awake()
    {
        musicAudioSource.clip = musicDataSO.music;
    }

    private void Start()
    {
        durationPerTime = 60f / musicDataSO.beatPerMinute * (musicDataSO.timeNumerator / musicDataSO.timeDenominator);
        samplePerTime = durationPerTime * musicAudioSource.clip.frequency;
        judgementRangeSample = musicAudioSource.clip.frequency * (judgementRangeFrame / 60f);
        judgementOffsetSample = musicAudioSource.clip.frequency * (judgementOffsetFrame / 60f);
        nextSample = musicAudioSource.clip.frequency * musicDataSO.offset;

        StartCoroutine(PlayMusic());
    }

    private void Update()
    {
        if (isTestMode && musicAudioSource.timeSamples >= nextSample)
        {
            StartCoroutine(PlayMetronome());
        }
    }
    public bool Judgement()
    {
        if (isCheckCurBeat == false) return false;
        judgementPointSample = musicAudioSource.timeSamples - judgementOffsetSample;
        isCheckCurBeat = false;
        return Mathf.Min(-(nextSample - samplePerTime - judgementPointSample), -(judgementPointSample - nextSample)) <= judgementRangeSample;
    }

    private IEnumerator PlayMetronome()
    {
        nextSample += samplePerTime;
        isCheckCurBeat = true;
        metronomeAudioSource.PlayOneShot(metronomeClip);

        yield return null;
    }

    private IEnumerator PlayMusic()
    {
        yield return null;

        musicAudioSource.Play();
    }
}
