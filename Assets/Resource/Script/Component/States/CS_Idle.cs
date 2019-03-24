using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Game.Actor;
using Snapshots = Game.Actor.Snapshots;
using Game.Network;

[RequireComponent(typeof(CS_StateMgr))]
public class CS_Idle : AvatarState {

    [Header("[Extra Properties]")]
    [HideInInspector]
    public C_Velocity _velocity;
    [HideInInspector]
    public CharacterController _characterController;
    [HideInInspector]
    public C_Camera _camera;
    [HideInInspector]
    public Identity _identity;

    private void OnEnable()
    {
        var stateMgr = GetComponent<CS_StateMgr>();
        //_name = "aim";
        stateMgr.RegState(_name, this);

        _camera = GetComponent<C_Camera>();
        _identity = GetComponent<Identity>();
        _velocity = GetComponent<C_Velocity>();
        _characterController = GetComponent<CharacterController>();
    }
    
    public override bool Listener() {
        
        return _velocity.idle;
    }

    public override void Enter() {
        if (_identity.IsPlayer)
        {
            var move = new Snapshots.Move()
            {
                Dfwd = _velocity.Dfwd,
                Dbwd = _velocity.Dbwd,
                Dright = _velocity.Dright,
                Dleft = _velocity.Dleft,
                position = this.transform.position
            };
            this._identity.Input(move);
        }
    }

    public override void OnUpdate() {
        
        Aspect.RotateToCameraY(_camera.Carryer, transform, 0.5f);
        _characterController.SimpleMove(Vector3.zero);
        
    }

    public override void Exit() { }

}
           
