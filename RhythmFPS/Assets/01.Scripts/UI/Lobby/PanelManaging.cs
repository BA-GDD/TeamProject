using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PanelManaging : MonoBehaviour
{
    [SerializeField] private RectTransform[] _panels;
    [SerializeField] private float moveValue;

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
