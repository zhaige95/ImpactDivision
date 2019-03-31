using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[RequireComponent(typeof(CS_StateMgr))]
public class CS_Idle : AvatarState {

    [Header("[Extra Properties]")]
    public C_Velocity _velocity;
    public CharacterController _characterController;
    public C_Camera _camera;

    private void OnEnable()
    {
        var stateMgr = GetComponent<CS_StateMgr>();
        //_name = "aim";
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
           
