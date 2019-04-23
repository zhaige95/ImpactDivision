using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UiEvent;

public class CS_Dead : AvatarState {

    C_Animator anim;
    C_Camera myCamera;
    C_Velocity velocity;
    C_IKManager iKManager;
    C_BattleMgr battleMgr;
    C_UiEventMgr uiMgr;
    C_Attributes attributes;
    C_WeaponHandle weaponHandle;
    C_BornProtector bornProtector;
    C_AttackListener attackListener;
    Timer timer = new Timer();
    public float recoverTime = 1f;
    public float vanishTime = 2;
    int process = 1;

    private void OnEnable()
    {
        anim = GetComponent<C_Animator>();
        uiMgr = GetComponent<C_UiEventMgr>();
        myCamera = GetComponent<C_Camera>();
        velocity = GetComponent<C_Velocity>();
        iKManager = GetComponent<C_IKManager>();
        battleMgr = GetComponent<C_BattleMgr>();
        attributes = GetComponent<C_Attributes>();
        weaponHandle = GetComponent<C_WeaponHandle>();
        bornProtector = GetComponent<C_BornProtector>();
        attackListener = GetComponent<C_AttackListener>();

        var stateMgr = GetComponent<CS_StateMgr>();
        //_name = "aim";
        stateMgr.RegState(_name, this);

    }

    public override void Enter()
    {
        base.Enter();
        process = 1;
        timer.Enter(vanishTime);

        attributes.isDead = true;
        velocity.isActive = false;
        velocity.Reset();
        anim.animator.enabled = false;
        attackListener.isActive = false;
        iKManager.SetAim(false);
        iKManager.SetHold(false);
        iKManager.SetDead(true);
        weaponHandle.Reset();
        weaponHandle.active = false;

    }

    public override void OnUpdate()
    {
        timer.Update();
        if (!timer.isRunning)
        {
            if (process == 1)
            {
                myCamera.SetFollowPlayer(false);
                iKManager.ragdollMgr.SetRagdollActive(false);
                this.transform.position = Battle.bornMgr.interimPoint.position;
                timer.Enter(recoverTime);
                process = 2;
            }
            else if (process == 2)
            {
                attributes.Recover();
                anim.animator.enabled = true;
                iKManager.SetDead(false);
                weaponHandle.targetWeapon = 1;

                timer.Enter();
                process = 3;
            }
            else if (process == 3)
            {
                this._exitTick = true;
            }
        }
    }

    public override void Exit()
    {
        base.Exit();
        
        var t = Battle.bornMgr.GetPoint(attributes.camp);
        this.transform.SetPositionAndRotation(t.position, t.rotation);
        if (!Battle.freezing)
        {
            velocity.isActive = true;
        }
        anim.animator.enabled = true;
        attackListener.Reset();

        attributes.isDead = false;
        weaponHandle.active = true;
        myCamera.SetFollowPlayer(true);
        myCamera.Reset(t);

        bornProtector.Enter();
        Debug.Log("exit dead state");
    }
}
