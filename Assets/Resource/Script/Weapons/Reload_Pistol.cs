using UnityEngine;
using System.Collections;
using UnityEngine.Animations;

public class Reload_Pistol : WeaponState
{
    [Header("[Components]")]
    public C_Animator _animator;
    public C_Velocity _velocity;
    public C_IKManager _iKManager;
    public CS_StateMgr _stateMgr;
    public C_WeaponHandle _weaponHandle;

    public AudioSource _audioSource;
    public WeaponAttribute _weaponAttribute;

    [Header("[Extra Properties]")]
    public AudioClip sound;
    public int animLayer = 5;
    public float exitTime = 0.8f;

    public override void Init(GameObject obj)
    {
        _animator = obj.GetComponent<C_Animator>();
        _velocity = obj.GetComponent<C_Velocity>();
        _iKManager = obj.GetComponent<C_IKManager>();
        _stateMgr = obj.GetComponent<CS_StateMgr>();
        _weaponHandle = obj.GetComponent<C_WeaponHandle>();

        _audioSource = GetComponent<AudioSource>();
        _weaponAttribute = GetComponent<WeaponAttribute>();
        
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

        _iKManager.SetAim(false);
        _iKManager.SetHold(false);

        _animator.animator.SetTrigger("reload");

        _audioSource.PlayOneShot(sound);

        _weaponHandle.locked = true;

    }

    public override void OnUpdate()
    {
        var _anim = _animator.animator;
        AnimatorStateInfo animatorInfo;
        animatorInfo = _anim.GetCurrentAnimatorStateInfo(animLayer);

        if (animatorInfo.IsTag("reload"))
        {
            if (animatorInfo.normalizedTime >= exitTime)
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
            }
        }
    }


    public override void Exit() {

        _iKManager.SetAim(true);
        _iKManager.SetHold(true);
        _weaponAttribute.reload = false;
        _weaponHandle.locked = false;
    }
}
 