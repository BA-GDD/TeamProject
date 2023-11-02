using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/MusicData")]
public class MusicDataSO : ScriptableObject
{
    public AudioClip music;
    public float beatPerMinute;
    public float timeNumerator;
    public float timeDenominator;
    public float offset;
}
