using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyUI : MonoBehaviour
{
    [SerializeField] private StartCollector[] _starCollectors;

    private void Start()
    {
        UIManager.Instanace.HandleStarPut += StarCollectorChoose;
        foreach (DifficultyType dt in Enum.GetValues(typeof(DifficultyType)))
        {
            MapInfo info = SaveManager.Instance.LoadMapInfoOrDefault(dt);
            if (info.Equals(default(MapInfo))) break;
            StarCollectorChoose(dt, info.starCount, false);
        }
    }

    private void OnDestroy()
    {
        UIManager.Instanace.HandleStarPut -= StarCollectorChoose;
    }

    private void StarCollectorChoose(DifficultyType dt, int starCount, bool isEasing)
    {
        _starCollectors[(int)dt].SetStar(starCount, isEasing);
    }
}
