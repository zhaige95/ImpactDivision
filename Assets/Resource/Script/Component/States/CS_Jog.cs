using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Game.Actor;
using Snapshots = Game.Actor.Snapshots;
using Game.Network;

[RequireComponent(typeof(CS_StateMgr))]
public class CS_Jog : AvatarState {

    [Header("[Components]")]
    [HideInInspector]
    public C_Camera _camera;
    [HideInInspector]
    public C_Animator _animator;
    [HideInInspector]
    public C_Velocity _velocity;
    [HideInInspector]
    public CS_StateMgr _stateMgr;
    [HideInInspector]
    public C_Attributes _attributes;
    [HideInInspector]
    public AudioSource _audioSource;
    [HideInInspector]
    public CharacterController _characterController;
    [HideInInspector]
    public Identity _identity;
    
   
    [Header("[Extra Properties]")]
    public float jogSpeed;
    public float walkSpeed;
    public AudioClip[] sounds;
    public float runSteptime;
    public float walkSteptime;
    public Timer timer = new Timer();

    Vector3 targetAngles = new Vector3();
    int directionIndex = 0;

    private void Start()
    {
        this._camera = GetComponent<C_Camera>();
        this._animator = GetComponent<C_Animator>();
        this._velocity = GetComponent<C_Velocity>();
        this._stateMgr = GetComponent<CS_StateMgr>();
        this._attributes = GetComponent<C_Attributes>();
        this._audioSource = GetComponent<AudioSource>();
        this._characterController = GetComponent<CharacterController>();
        this._identity = GetComponent<Identity>();
        this._stateMgr.RegState(_name, this);
        //_name = "aim";

        //this._identity = this.GetComponent<Identity>();
        //this._identity.BindEvent(typeof(Snapshots.Move), this.Move);
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
        if (_velocity.armed)
        {
            _velocity.currentSpeed = _velocity.aiming ? walkSpeed : jogSpeed;

            _animator.animator.SetBool("idle", _velocity.idle);
        }
    }

    public override void OnUpdate() {


        if (!_attributes.isDead)
        {
            var _anim = _animator.animator;
            
            if (_velocity.armed)
            {
                _animator.AddEvent("Dfwd", _velocity.fwd);
                _animator.AddEvent("Dright", _velocity.right);

                _animator.AddEvent("aim", _velocity.aiming ? 1f : 0f);
                _velocity.currentSpeed = _velocity.aiming ? walkSpeed : jogSpeed;

                Aspect.RotateToCameraY(_camera.Carryer, transform, 0.5f);
                var currentSpeed = _velocity.currentSpeed;
                _characterController.Move(transform.forward * currentSpeed * _attributes.rate * _velocity.fwd * Time.deltaTime +
                            transform.right * currentSpeed * _attributes.rate * _velocity.right * Time.deltaTime);
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

    private void FixedUpdate()
    {
        if (_identity.IsPlayer && this._active)
        {
            SendSnapshot();
        }
    }

    public void Simulate()
    {
        if (this._active)
        {
            //Aspect.RotateToCameraY(_camera.Carryer, transform, 0.5f);
            //var currentSpeed = _velocity.currentSpeed;
            //_characterController.Move(transform.forward * currentSpeed * _attributes.rate * _velocity.fwd * Time.deltaTime +
            //            transform.right * currentSpeed * _attributes.rate * _velocity.right * Time.deltaTime);
        }
    }


    public override void Exit() {
        _animator.animator.SetBool("idle", _velocity.idle);
        _animator.AddEvent("Dfwd", 0);
        _animator.AddEvent("Dright", 0);
    }

    void SendSnapshot()
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

    //private void Move(Snapshot snapshot)
    //{
    //    var move = snapshot as Snapshots.Move;

    //    if (ServerMgr.Active)
    //    {
    //        move.position = this.transform.position;
    //    }
    //    else
    //    {
    //        this.transform.position = move.position;
    //    }
    //    _velocity.Dfwd = move.Dfwd;
    //    _velocity.Dbwd = move.Dbwd;
    //    _velocity.Dright = move.Dright;
    //    _velocity.Dleft = move.Dleft;
    //}

}
