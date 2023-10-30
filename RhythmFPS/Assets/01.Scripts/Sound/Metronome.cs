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
    private float oneTimeDuration;
    private float samplePerTime;
    private float nextSample;

    private void Awake()
    {
        musicAudioSource.clip = musicDataSO.music;
    }

    private void Start()
    {
        oneTimeDuration = 60f / musicDataSO.beatPerMinute * (musicDataSO.timeNumerator / musicDataSO.timeDenominator);
        samplePerTime = oneTimeDuration * musicAudioSource.clip.frequency;
        nextSample = musicAudioSource.clip.frequency * musicDataSO.offset;

        StartCoroutine(PlayMusic());
    }

    private void Update()
    {
        if (musicAudioSource.timeSamples >= nextSample)
        {
            StartCoroutine(PlayMetronome());
        }
    }

    private IEnumerator PlayMetronome()
    {
        nextSample += samplePerTime;

        metronomeAudioSource.PlayOneShot(metronomeClip);

        yield return null;
    }

    private IEnumerator PlayMusic()
    {
        yield return null;

        musicAudioSource.Play();
    }
}
