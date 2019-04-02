using UnityEngine;
using System.Collections;
using System;
using Data;

[System.Serializable]
public class WeaponState : MonoBehaviour
{
    [Header("[Basic Properties]")]
    public string _name = "!!!";
    public StateLayer layer;
    public bool _active = false;
    public bool _unique = false;
    public bool _pause = false;
    public bool _enterTick = false;
    public bool _exitTick = false;

    private void OnEnable()
    {
        GetComponent<WeaponAttribute>().RegState(_name, this);
    }

    public virtual void Init(GameObject obj) { }

    public virtual bool Listener() { return false; }

    public virtual void Enter()
    {
        _active = true;
    }

    public virtual void OnUpdate() { }

    public void StateExit()
    {
        Exit();
        _active = false;
        _enterTick = false;
        _exitTick = false;
    }

    public virtual void Exit() {
        _active = false;
        _enterTick = false;
        _exitTick = false;
    }



}
 