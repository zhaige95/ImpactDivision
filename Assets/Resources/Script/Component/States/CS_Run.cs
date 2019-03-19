using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[RequireComponent(typeof(CS_StateMgr))]
public class CS_Run : AvatarState {

    [Header("[Components]")]
    public C_Camera _camera;
    public C_Animator _animator;
    public C_Velocity _velocity;
    public CS_StateMgr _stateMgr;
    public C_Attributes _attributes;
    public AudioSource _audioSource;
    public CharacterController _characterController;

    [Header("[Extra Properties]")]
    public float speed;
    
    Vector3 targetAngles = new Vector3();
    int directionIndex = 0;


    private void OnEnable()
    {
        var stateMgr = GetComponent<CS_StateMgr>();
        //_name = "aim";
        stateMgr.RegState(_name, this);
        
    }
    
    public override bool Listener() {

        if (_velocity.Drun)
        {
            return true;
        }

        return false;
    }

    public override void Enter()
    {
        if (_velocity.armed)
        {
            _velocity.currentSpeed = speed;
            _animator.animator.SetBool("run", true);
        }
    }

    public override void OnUpdate() {


        if (!_attributes.isDead)
        {
            var _anim = _animator.animator;

            if (_velocity.armed)
            {
                var currentSpeed = _velocity.currentSpeed;
                
                _characterController.Move(transform.forward * currentSpeed * _attributes.rate * Time.deltaTime );

            }
            SetFreeDirection();
            _characterController.SimpleMove(Vector3.zero);

        }
        else
        {
            this._exitTick = true;
        }

        if (!_velocity.Drun)
        {
            this._exitTick = true;
        }
        
    }

    public override void Exit() {
        //_velocity.Drun = false;
        _animator.animator.SetBool("run", false);
    }


    void SetFreeDirection()
    {
        // follow the camera when movecharacter
        if (_velocity.Dfwd)
        {

            directionIndex = 1;
            if (_velocity.Dleft)
            {
                directionIndex = 8;
            }
            if (_velocity.Dright)
            {
                directionIndex = 2;
            }
        }
        if (_velocity.Dbwd)
        {
            directionIndex = 5;
            if (_velocity.Dleft)
            {
                directionIndex = 6;
            }
            if (_velocity.Dright)
            {
                directionIndex = 4;
            }
        }
        if (_velocity.Dleft)
        {
            directionIndex = 7;
            if (_velocity.Dfwd)
            {
                directionIndex = 8;
            }
            if (_velocity.Dbwd)
            {
                directionIndex = 6;
            }
        }
        if (_velocity.Dright)
        {
            directionIndex = 3;
            if (_velocity.Dfwd)
            {
                directionIndex = 2;
            }
            if (_velocity.Dbwd)
            {
                directionIndex = 4;
            }
        }

        if (directionIndex != 0)
        {
            switch (directionIndex)
            {
                case 1:
                    targetAngles.Set(0, _camera.Carryer.localEulerAngles.y, 0);
                    break;
                case 2:
                    targetAngles.Set(0, _camera.Carryer.localEulerAngles.y + 45, 0);
                    break;
                case 3:
                    targetAngles.Set(0, _camera.Carryer.localEulerAngles.y + 90, 0);
                    break;
                case 4:
                    targetAngles.Set(0, _camera.Carryer.localEulerAngles.y + 135, 0);
                    break;
                case 5:
                    targetAngles.Set(0, _camera.Carryer.localEulerAngles.y + 180, 0);
                    break;
                case 6:
                    targetAngles.Set(0, _camera.Carryer.localEulerAngles.y + -135, 0);
                    break;
                case 7:
                    targetAngles.Set(0, _camera.Carryer.localEulerAngles.y - 90, 0);
                    break;
                case 8:
                    targetAngles.Set(0, _camera.Carryer.localEulerAngles.y - 45, 0);
                    break;
            }

        }
        if (directionIndex != 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(targetAngles), 20f * Time.deltaTime);
        }
    }

}
           
