using UnityEngine;
using System.Collections;
using UnityEngine.Animations;

public class NV4_Reload : WeaponState
{
    [Header("[Components]")]
    public C_Animator _animator;
    public C_Velocity _velocity;
    public C_IKManager _iKManager;
    public C_WeaponHandle _weaponHandle;

    public CS_StateMgr _stateMgr;

    public AudioSource _audioSource;
    public WeaponAttribute _weaponAttribute;
    public Animator _ownAnimator;

    [Header("[Extra Properties]")]
    public Transform magIK;
    public AudioClip[] sounds;
    public int animLayer = 5;
    public float reloadTime;
    public Timer timer;
    public int process = 1;

    public override void Init(GameObject obj)
    {
        _animator = obj.GetComponent<C_Animator>();
        _velocity = obj.GetComponent<C_Velocity>();
        _iKManager = obj.GetComponent<C_IKManager>();
        _weaponHandle = obj.GetComponent<C_WeaponHandle>();

        _stateMgr = obj.GetComponent<CS_StateMgr>();

        _audioSource = GetComponent<AudioSource>();
        _weaponAttribute = GetComponent<WeaponAttribute>();
        _ownAnimator = GetComponent<Animator>();

        timer = new Timer();
    }

    public override bool Listener() {

        
        if (_velocity.Dreload)
        {
            if (_weaponAttribute.runtimeMag < _weaponAttribute.mag)
            {
                return true;
            }
        }

        return false;
    }

    public override void Enter() {

        _weaponAttribute.reload = true;

        process = 1;
        
        _animator.animator.SetTrigger("reload");
        _ownAnimator.SetTrigger("reload");
        _iKManager.SetAim(false);
        _iKManager.SetHoldTarget(magIK);
        _weaponHandle.locked = true;

    }

    public override void OnUpdate()
    {
        var _anim = _animator.animator;
        //var constraint = _weaponAttribute.magObj.GetComponent<ParentConstraint>();

        AnimatorStateInfo animatorInfo;
        animatorInfo = _anim.GetCurrentAnimatorStateInfo(animLayer);

        if (process == 1)
        {

            _audioSource.PlayOneShot(sounds[0]);
            //constraint.constraintActive = true;
            process = 2;

        }
        
        if (animatorInfo.IsTag("reload"))
        {
            // new coding ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
            if (process == 2)
            {
                if (animatorInfo.normalizedTime >= 0.45f)
                {
                    _audioSource.PlayOneShot(sounds[1]);
                    process = 3;
                }
            }
            else if (process == 3)
            {
                if (animatorInfo.normalizedTime >= 0.9f)
                {
                    if (_weaponAttribute.bore)
                    {
                        _weaponAttribute.runtimeMag = _weaponAttribute.mag;
                    }
                    else
                    {
                        _weaponAttribute.runtimeMag = _weaponAttribute.mag - 1;
                        _weaponAttribute.bore = true;
                    }
                    this._exitTick = true;
                    process = 6;
                }
            }
            // new coding ↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑

            //if (process == 2)
            //{

            //    if (animatorInfo.normalizedTime >= 0.1f)
            //    {
            //        _iKManager.SetHold(false);
            //        process = 3;
            //    }
            //}
            //if (process == 3)
            //{
            //    if (animatorInfo.normalizedTime >= 0.45f)
            //    {
            //        constraint.constraintActive = false;
            //        _weaponAttribute.magObj.transform.localPosition = Vector3.zero;
            //        _weaponAttribute.magObj.transform.localEulerAngles = Vector3.zero;
            //        _audioSource.PlayOneShot(sounds[1]);
            //        _iKManager.SetHold(true);
            //        process = 4;
            //    }
            //}
            //if (process == 4)
            //{
            //    if (animatorInfo.normalizedTime >= 0.6f)
            //    {
            //        _iKManager.SetHold(false);

            //        _iKManager.SetHoldTarget(_weaponAttribute.holdPoint);
            //        process = 5;
            //    }
            //}
            //if (process == 5)
            //{
            //    if (animatorInfo.normalizedTime >= 0.9f)
            //    {
            //        if (_weaponAttribute.bore)
            //        {
            //            _weaponAttribute.runtimeMag = _weaponAttribute.mag;
            //        }
            //        else
            //        {
            //            _weaponAttribute.runtimeMag = _weaponAttribute.mag - 1;
            //            _weaponAttribute.bore = true;
            //        }
            //        this._exitTick = true;
            //        process = 6;
            //    }
            //}

        }
    }
    public override void Exit() {
        Debug.Log("exit reload");
        //_weaponAttribute.magObj.GetComponent<ParentConstraint>().constraintActive = false;
        //_weaponAttribute.magObj.transform.localPosition = Vector3.zero;
        //_weaponAttribute.magObj.transform.localEulerAngles = Vector3.zero;

        _iKManager.SetHold(true);
        _iKManager.SetAim(true);
        _weaponAttribute.reload = false;
        _weaponHandle.locked = false;

        //if (process != 6)
        //{
            _iKManager.SetHoldTarget(_weaponAttribute.holdPoint);
        //}

    }
}
 