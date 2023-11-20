using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BTVisual;

public class CheckBeatNode : ActionNode
{
    public int beatCnt = 0;
    private bool isChecked = false;
    private int _curCnt = 0;

    protected override void OnStart()
    {
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        if(beatCnt == _curCnt)
        {
            return State.SUCCESS;
        }
        if(!isChecked && RhythmManager.instance.Judgement() == true)
        {
            isChecked = true;
            _curCnt++;
        }
        else if(RhythmManager.instance.Judgement() == false)
        {
            isChecked=false;
        }


        return State.RUNNING;
    }
}
