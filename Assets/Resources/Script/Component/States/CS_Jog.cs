using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[RequireComponent(typeof(CS_StateMgr))]
public class CS_Jog : AvatarState {

    [Header("[Components]")]
    C_Camera _camera;
    C_Animator _animator;
    C_Velocity _velocity;
    C_Attributes _attributes;
    AudioSource _audioSource;
    CharacterController _characterController;

    [Header("[Extra Properties]")]
    public float jogSpeed;
    public float walkSpeed;
    public AudioClip[] sounds;
    public float runSteptime;
    public float walkSteptime;
    public Timer timer = new Timer();
    

    private void OnEnable()
    {
        _camera = GetComponent<C_Camera>();
        _animator = GetComponent<C_Animator>();
        _velocity = GetComponent<C_Velocity>();
        _attributes = GetComponent<C_Attributes>();
        _audioSource = GetComponent<AudioSource>();
        _characterController = GetComponent<CharacterController>();

        var stateMgr = GetComponent<CS_StateMgr>();
        //_name = "aim";
        stateMgr.RegState(_name, this);
        
    }
    
    public override bool Listener() {

        if (!_velocity.idle && !_attributes.isDead)
        {
            if (!_velocity.crouch && !_velocity.Drun)
            {
                return true;
            }
        }

        return false;
    }

    public override void Enter() {
        base.Enter();
        if (_velocity.armed)
        {
            _velocity.currentSpeed = _velocity.aiming ? walkSpeed : jogSpeed;
            _animator.animator.SetBool("idle", _velocity.idle);

            timer.Enter(_velocity.aiming?walkSteptime: runSteptime);
            
        }
    }

    public override void OnUpdate() {


        if (!_attributes.isDead)
        {
            timer.Update();

            if (!timer.isRunning)
            {
                Sound.PlayOneShot(_audioSource, sounds);
                timer.Enter(_velocity.aiming ? walkSteptime : runSteptime);
            }

            if (_velocity.armed)
            {
                _animator.AddEvent("Dfwd", _velocity.fwd);
                _animator.AddEvent("Dright", _velocity.right);
                
                if (_velocity.isLocalPlayer)
                {
                    _animator.AddEvent("aim", _velocity.aiming ? 1f : 0f);
                    _velocity.currentSpeed = _velocity.aiming ? walkSpeed : jogSpeed;
                    var currentSpeed = _velocity.currentSpeed;
                    Aspect.RotateToCameraY(_camera.Carryer, transform, 0.5f);
                    _characterController.Move(transform.forward * currentSpeed * _attributes.rate * _velocity.fwd * Time.deltaTime +
                                        transform.right * currentSpeed * _attributes.rate * _velocity.right * Time.deltaTime);
                }
                
            }
            else
            {
                //if (!_velocity.idle)
                //{
                //    currentSpeed = _velocity.Drun ? _attributes.runSpeed : _attributes.walkSpeed;
                //    _characterController.Move(transform.forward * currentSpeed * _attributes.rate * Time.deltaTime);
                //    // follow the camera when move character
                //    SetFreeDirection();
                //}
            }
            _characterController.SimpleMove(Vector3.zero);

        }
        else
        {
            this._exitTick = true;
        }

        if (_velocity.idle)
        {
            this._exitTick = true;
        }
    }

    public override void Exit()
    {
        base.Exit();
        timer.Exit();
        _animator.animator.SetBool("idle", _velocity.idle);
        _animator.AddEvent("Dfwd", 0);
        _animator.AddEvent("Dright", 0);
    }



}
           
