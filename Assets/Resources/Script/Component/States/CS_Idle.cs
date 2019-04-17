using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[RequireComponent(typeof(CS_StateMgr))]
public class CS_Idle : AvatarState {

    [Header("[Components]")]
    C_Velocity _velocity;
    CharacterController _characterController;
    C_Camera _camera;

    private void OnEnable()
    {
        _velocity = GetComponent<C_Velocity>();
        _characterController = GetComponent<CharacterController>();
        _camera = GetComponent<C_Camera>();

        var stateMgr = GetComponent<CS_StateMgr>();
        stateMgr.RegState(_name, this);
        
    }
    
    public override bool Listener() {
        return _velocity.idle;
    }

    public override void OnUpdate() {
        if (_velocity.isLocalPlayer)
        {
            Aspect.RotateToCameraY(_camera.Carryer, transform, 0.5f);
        }

        _characterController.SimpleMove(Vector3.zero);
    }


}
           
