using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PanelManaging : MonoBehaviour
{
    [Header("사이즈 셋팅")]
    [SerializeField] private RectTransform[] _panels;
    [SerializeField] private float moveValue;

    [Header("맵 페널 셋팅")]
    public MapEnterPanel selectMapEnterPanel;

    public void EnterPointPnanel(RectTransform trm)
    {
        int idx = 3;
        for(int i = 0; i < _panels.Length; i++)
        {
            if(_panels[i] == trm)
            {
                idx = i;
                continue;
            }

            if(idx < i)
            {
                PlayPanel pp = _panels[i].GetComponent<PlayPanel>();
                pp.PanelMove(-moveValue);
            }
        }
    }
    public void ExitPointPnanel(RectTransform trm)
    {
        int idx = 3;
        for (int i = 0; i < _panels.Length; i++)
        {
            if (_panels[i] == trm)
            {
                idx = i;
                continue;
            }

            if (idx < i)
            {
                PlayPanel pp = _panels[i].GetComponent<PlayPanel>();
                pp.PanelMove(0);
            }
        }
    }
}
