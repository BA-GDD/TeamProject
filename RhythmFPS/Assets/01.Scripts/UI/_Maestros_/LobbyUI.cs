using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyUI : MonoBehaviour
{
    [SerializeField] private StartCollector[] _starCollectors;

    private void Start()
    {
        UIManager.Instanace.HandleStarPut += StarCollectorChoose;
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
