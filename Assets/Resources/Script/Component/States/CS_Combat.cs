using UnityEngine;
using System.Collections;

public class CS_Combat : AvatarState
{
    public C_Animator animator;
    public C_Velocity velocity;

    private void OnEnable()
    {
        var stateMgr = GetComponent<CS_StateMgr>();
        stateMgr.RegState(_name, this);

        animator = GetComponent<C_Animator>();
        velocity = GetComponent<C_Velocity>();
    }

    public override bool Listener()
    {
        
        return base.Listener();
    }

    public override void Enter()
    {
        base.Enter();

    }

    public override void OnUpdate()
    {

    }

    public override void Exit()
    {
        base.Exit();
    }



}
