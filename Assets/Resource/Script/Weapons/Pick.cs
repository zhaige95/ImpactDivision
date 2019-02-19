using UnityEngine;
using System.Collections;
using UnityEngine.Animations;

public class Pick : WeaponState
{
    [Header("[Extra Properties]")]
    public bool _isPicked = false;
    public C_Animator _animator;
    public C_Velocity _velocity;
    public C_WeaponHandle _weaponHandle;
    public C_IKManager _iKManager;
    public AudioSource _audio;
    public WeaponAttribute _weaponAttribute;
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
        pickTimer = new Timer();
        endTimer = new Timer();
    }

    public override bool Listener() {

        if (_isPicked != _weaponAttribute.active)
        {
            return true;
        }

        return false;
    }

    public override void Enter() {
        process = 1;
        _iKManager.SetAim(false);
        if (_weaponAttribute.active)
        {
            _animator.animator.SetTrigger("pickWeapon");
            _animator.animator.SetInteger("weaponTarget", (int)_weaponAttribute.type);
            _animator.AddEvent("weaponType", (int)_weaponAttribute.type);
            _iKManager.SetHold(false);
            _iKManager.SetHoldTarget(_weaponAttribute.holdPoint);
            endTimer.Enter(endTime);
            pickTimer.Enter(pickTime);
            if ((int) _weaponAttribute.magType == 1)
            {
                _weaponAttribute.magObj.GetComponent<ParentConstraint>().SetSource(0, new ConstraintSource()
                    {
                        sourceTransform = _weaponHandle.leftHand,
                        weight = 1
                    }
                );
            }
            _weaponHandle.locked = true;

        }
        else
        {
            _weaponAttribute.constraint.weight = 1;
            _iKManager.SetHold(false);
            _weaponAttribute.ready = false;
            this._exitTick = true;
        }

    }

    public override void OnUpdate()
    {
        endTimer.Update();
        pickTimer.Update();

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
            _iKManager.SetAim(true);
            _iKManager.SetHold(true);
            _weaponAttribute.ready = true;
            _weaponHandle.locked = false;
            this._exitTick = true;
        }
    }
    public override void Exit() {
        _isPicked = _weaponAttribute.active;
    }
}
 