using UnityEngine;
using System.Collections;
using UnityEngine.Animations;
using UiEvent;
using Data;

public class Pick : WeaponState
{
    [Header("[Components]")]
    public AudioSource _audio;
    public C_UiEventMgr _uiMgr;
    public C_Velocity _velocity;
    public C_Animator _animator;
    public C_IKManager _iKManager;
    public C_WeaponHandle _weaponHandle;
    public WeaponAttribute _weaponAttribute;
    [Header("[Extra Properties]")]
    public bool _isPicked = false;
    public AudioClip[] sounds;
    public float pickTime;
    public float endTime;
    public int process = 1;
    public Timer pickTimer;
    public Timer endTimer;
    public TransformMark holdOffset;
    public Vector3 axis;

    public override void Init(GameObject obj)
    {
        _animator = obj.GetComponent<C_Animator>();
        _velocity = obj.GetComponent<C_Velocity>();
        _weaponHandle = obj.GetComponent<C_WeaponHandle>();
        _iKManager = obj.GetComponent<C_IKManager>();
        _audio = obj.GetComponent<AudioSource>();
        _weaponAttribute = GetComponent<WeaponAttribute>();
        _uiMgr = obj.GetComponent<C_UiEventMgr>();

        pickTimer = new Timer();
        endTimer = new Timer();
    }

    public override bool Listener() {

        //if (_isPicked != _weaponAttribute.active)
        //{
        //    return true;
        //}

        return false;
    }

    public override void Enter()
    {
        base.Enter();
        process = 1;
        if (_weaponAttribute.active)
        {
            _animator.animator.SetTrigger("pickWeapon");
            _animator.animator.SetInteger("weaponTarget", (int)_weaponAttribute.type);
            _animator.AddEvent("weaponType", (int)_weaponAttribute.type);
            _iKManager.SetHoldTarget(_weaponAttribute.holdPoint);
            endTimer.Enter(endTime);
            pickTimer.Enter(pickTime);

            _weaponHandle.locked = true;
            var cutMsg = new UiEvent.UiMsgs.WeaponCut()
            {
                texture = _weaponAttribute.cutPicInBattle
            };
            _uiMgr.SendEvent(cutMsg);

            var ammoMsg = new UiEvent.UiMsgs.Ammo()
            {
                ammo = _weaponAttribute.runtimeMag + (_weaponAttribute.bore ? 1f : 0f),
                mag = _weaponAttribute.mag
            };
            _uiMgr.SendEvent(ammoMsg);
        }
        else
        {
            _weaponAttribute.constraint.weight = 1;
            foreach (var state in _weaponAttribute.states.Values)
            {
                if (!state._name.Equals(this._name))
                {
                    if (state._active)
                    {
                        state.Exit();
                    }
                }
            }
            _weaponAttribute.ready = false;
            this._exitTick = true;
        }
        _iKManager.SetAim(false);
        _iKManager.SetHold(false);

    }

    public override void OnUpdate()
    {
        if (!this._exitTick)
        {
            endTimer.FixedUpdate();
            pickTimer.FixedUpdate();

            if (!pickTimer.isRunning && process == 1)
            {
                Sound.PlayOneShot(_audio, sounds);
                _weaponAttribute.constraint.weight = 0;

                _weaponHandle.handPoint.localPosition = _weaponAttribute.holdOffset._position;
                _weaponHandle.handPoint.localEulerAngles = _weaponAttribute.holdOffset._rotation;
                _weaponHandle.shootPoint.GetComponent<ParentConstraint>().SetSource(0,
                    new ConstraintSource()
                    {
                        sourceTransform = _weaponAttribute.shootPoint,
                        weight = 1
                    });

                _iKManager.targetAxis = axis;
                _iKManager.aimIK.solver.axis = axis;
                transform.localPosition = Vector3.zero;
                transform.localEulerAngles = Vector3.zero;
                _velocity.armed = true;
                process = 2;
            }

            if (!endTimer.isRunning)
            {
                if (_velocity.Drun)
                {
                    _iKManager.SetAim(false);
                    if (_weaponAttribute.type == WeaponType.Pistol)
                    {
                        _iKManager.SetHold(false);
                    }
                    else if (_weaponAttribute.type == WeaponType.Rifle)
                    {
                        _iKManager.SetHold(true);
                    }
                }
                else
                {
                    _iKManager.SetAim(true);
                    _iKManager.SetHold(true);
                }

                _weaponAttribute.ready = true;
                _weaponHandle.locked = false;
                this._exitTick = true;
            }
        }
    }
    public override void Exit()
    {
        base.Exit();
        _isPicked = _weaponAttribute.active;
        
    }
}
 