using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PanelManaging : MonoBehaviour
{
    [Header("사이즈 셋팅")]
    [SerializeField] private PlayPanel[] _panels;
    [SerializeField] private float moveValue;

    [Header("맵 페널 셋팅")]
    public PlayPanel selectPlayPanel;
    public MapEnterPanel selectMapEnterPanel;
    public RectTransform activePanelMark;

    public bool IsExistSelectPanel()
    {
        for (int i = 0; i < _panels.Length; i++)
        {
            if (_panels[i].isSelect)
            {
                return true;
            }
        }
        return false;
    }

    public PlayPanel FindSelectPanel()
    {
        for (int i = 0; i < _panels.Length; i++)
        {
            if (_panels[i].isSelect)
            {
                return _panels[i];
            }
        }
        return null;
    }

    public void RefreshPanelSize()
    {
        for(int i = 0; i < _panels.Length; i++)
        {
            if (_panels[i].isSelect)
            {
                _panels[i].isSelect = false;
                //_panels[i].OnExitPointerInThisPanel();
                break;
            }
        }
    }

    public void EnterPointPnanel(PlayPanel pp)
    {
        int idx = 3;
        for(int i = 0; i < _panels.Length; i++)
        {
            if(_panels[i] == pp)
            {
                idx = i;
                continue;
            }

            if(idx < i)
            {
                _panels[i].PanelMove(-moveValue);
            }
        }
    }
    public void ExitPointPnanel(PlayPanel pp)
    {
        int idx = 3;
        for (int i = 0; i < _panels.Length; i++)
        {
            if (_panels[i] == pp)
            {
                idx = i;
                continue;
            }

            if (idx < i)
            {
                _panels[i].PanelMove(0);
            }
        }
    }
}
