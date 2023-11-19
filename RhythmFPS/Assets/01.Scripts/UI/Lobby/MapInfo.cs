using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

[System.Serializable]
public struct MapInfo
{
    public DifficultyType difficultyType;
    public int starCount;
    public int maxCombo;
    public int bestScore;
}
