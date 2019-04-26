using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using UiEvent;
using System;

public class C_Attributes : MonoBehaviour {
    
    public Entity entity;
    public bool isLocalPlayer = false;
    public float HP = 100f;
    public float HPMax = 100f;
    public float runSpeed = 4f;
    public float jogSpeed = 3.5f;
    public float walkSpeed = 3f;
    public float rate = 1.5f;
    public float jumpForce = 3.2f;
    public float jumpForce2 = 3.2f;
    public int camp = 2;
    public bool isDead = false;
    public Timer timer = new Timer();
    public float recoverTime = 3f;
    [Header("[Component]")]
    C_UiEventMgr uiMgr;
    Animator animator;
    C_AttackListener attackListener;

    //-------------
    public Action OnDead;
    public Action OnRecover;

    private void Awake()
    {
        uiMgr = GetComponent<C_UiEventMgr>();
        animator = GetComponent<C_Animator>().animator;
        attackListener = GetComponent<C_AttackListener>();
    }

    [PunRPC]
    public void Demaged(float demage, string operation)
    {
        if (operation.Equals("-"))
        {
            HP -= demage;
        }
        else if (operation.Equals("+"))
        {
            HP += demage;
        }
        HP = Mathf.Clamp(HP, 0f, HPMax);

        if (HP <= 0f)
        {
            OnDead?.Invoke();
        }

        var hpMsg = new UiEvent.UiMsgs.Hp()
        {
            hp = HP,
            hpMax = HPMax
        };
        uiMgr.SendEvent(hpMsg);
        
    }

    [PunRPC]
    public void Demaged(float demage)
    {
        this.Demaged(demage, "-");

        attackListener.PlayBeAttackedSound();
        animator.SetTrigger("hit");

        var bloodMsg = new UiEvent.UiMsgs.Blood();
        uiMgr.SendEvent(bloodMsg);

    }

    public void Recover()
    {
        this.Demaged(HPMax, "+");
        this.isDead = false;
        OnRecover?.Invoke();
    }

}
