using Core;
using System;
using System.Collections.Generic;
using UnityEngine;

public class AgentController : MonoBehaviour
{
    [SerializeField]private InputReader _inputReader;

    private Dictionary<StateType, IState> _stateDictionary = null;  //가지고 있는 상태들 저장
    [SerializeField]private IState _currentState; //현재 상태 저장

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;

        _stateDictionary = new Dictionary<StateType, IState>();
        Transform stateTrm = transform.Find("States");

        foreach( StateType state in Enum.GetValues(typeof (StateType)) )
        {
            IState stateScript = stateTrm.GetComponent($"{state}State") as IState;
            if(stateScript == null)
            {
                Debug.LogError($"There is no script : {state}");
                return;
            }
            stateScript.SetUp(transform,_inputReader);
            _stateDictionary.Add(state, stateScript);
        }
    }
    private void Start()
    {
        ChangeState(StateType.Normal);
    }

    public void ChangeState(StateType type)
    {
        _currentState?.OnExitState(); //현재 상태 나가고
        _currentState = _stateDictionary[type];
        _currentState?.OnEnterState(); //다음상태 시작
    }

    private void Update()
    {
        _currentState.UpdateState();
    }

}