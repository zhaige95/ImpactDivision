using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[RequireComponent(typeof(CS_StateMgr))]
public class CS_Jump : AvatarState {

    [Header("[Components]")]
    public C_Camera _camera;
    public C_Velocity _velocity;
    public C_Animator _animator;
    public C_Attributes _attributes;
    public C_OnGroundSensor _onGroundSensor;
    public AudioSource _audioSource;
    public CharacterController _characterController;

    [Header("[Extra Properties]")]
    public Timer timer = new Timer();
    public float activeTime = 0.4f;
    public int part = 1;
    public float gravity = 20f;
    public float force = 5f;
    public int process = 1;
    public AudioClip[] sounds;


    private void OnEnable()
    {
        var stateMgr = GetComponent<CS_StateMgr>();
        //_name = "aim";
        stateMgr.RegState(_name, this);
        
    }
    
    public override bool Listener() {

        if (_velocity.Djump)
        {
            return true;
        }

        return false;
    }

    public override void Enter()
    {
        if (_velocity.crouch)
        {
            _velocity.crouch = false;
            _animator.AddEvent("crouch", 0f);
            this._exitTick = true;
        }
        else
        {
            timer.Enter(activeTime);
            process = 1;
            force = _attributes.jumpForce;
            _animator.animator.SetBool("jump", true);
            _velocity.jumping = true;
        }
    }

    public override void OnUpdate() {


        if (!_attributes.isDead)
        {
            timer.Update();

            var currentSpeed = _velocity.currentSpeed;

            if (_velocity.armed)
            {
                Aspect.RotateToCameraY(_camera.Carryer, transform, 0.5f);
                _characterController.Move(
                   transform.forward * currentSpeed * _attributes.rate * _velocity.fwd * Time.deltaTime +
                   transform.right * currentSpeed * _attributes.rate * _velocity.right * Time.deltaTime +
                   transform.up * force * _attributes.rate * Time.deltaTime
               );
            }

            force -= (gravity * Time.deltaTime);

            if (process == 1 && _velocity.Djump && !timer.isRunning)
            {
                process = 2;
                if (force < 0)
                {
                    force = _attributes.jumpForce2;
                }
                else
                {
                    force += _attributes.jumpForce2;
                }

                _animator.animator.SetTrigger("tjump");
            }


            if (force < 0 && _onGroundSensor.isGrounded)
            {
                this._exitTick = true;
            }

        }
       
    }

    public override void Exit() {

        _animator.animator.SetBool("jump", false);
        _velocity.jumping = false;
    }
    
    
}
           
