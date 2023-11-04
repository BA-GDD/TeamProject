using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;

public abstract class CommonState : MonoBehaviour, IState
{
    public abstract void OnEnterState();

    public abstract void OnExitState();

    public abstract void UpdateState();
    //protected AgentAnimator animator;

    //protected AgentInput agentInput;
    protected AgentController agentController;
    protected AgentMovement agentMovement;
    protected AgentRotater agentRotater;
    protected AgentWeapon agentWeapon;
    protected AgentAbility agentAbility;
    protected Camera mainCam;
    protected InputReader inputReader;

    public virtual void SetUp(Transform agentRoot, InputReader inputReader)
    {
        //animator = agentRoot.Find("Visual").GetComponent<AgentAnimator>();
        //agentInput = agentRoot.GetComponent<AgentInput>();
        agentController = agentRoot.GetComponent<AgentController>();
        agentMovement = agentRoot.GetComponent<AgentMovement>();
        agentRotater = agentRoot.GetComponent<AgentRotater>();
        agentWeapon = agentRoot.GetComponent<AgentWeapon>();
        agentAbility = agentRoot.GetComponent<AgentAbility>();
        mainCam = Define.MainCam;

        this.inputReader = inputReader;
    }
    public void OnHitHandle(Vector3 hitPoint, Vector3 Normal)
    {

    }
}
