using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using System;
using Data;

public class AvatarState : MonoBehaviour {

    public string _name = "";
    public StateLayer layer;
    public bool _active = false;
    public bool _unique = false;
    public bool _pause = false;
    public bool _enterTick = false;
    public bool _exitTick = false;

    private void OnEnable()
    {
        var stateMgr = GetComponent<CS_StateMgr>();
        stateMgr.RegState(_name, this);
    }

    public virtual bool Listener() { return false; }

    public virtual void Enter() {
        _active = true;
        _exitTick = false;
        _enterTick = false;
    }

    public virtual void OnUpdate() { }

    public virtual void Exit() {
        _active = false;
        _exitTick = false;
        _enterTick = false;
    }

}
