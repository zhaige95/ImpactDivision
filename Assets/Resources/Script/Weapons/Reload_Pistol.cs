using UnityEngine;
using System.Collections;
using UnityEngine.Animations;
using UiEvent;

public class Reload_Pistol : WeaponState
{
    [Header("[Components]")]
    C_Animator _animator;
    C_Velocity _velocity;
    C_IKManager _iKManager;
    CS_StateMgr _stateMgr;
    C_WeaponHandle _weaponHandle;
    C_UiEventMgr _uiMgr;
    PhotonView _photonView;

    AudioSource _audioSource;
    WeaponAttribute _weaponAttribute;

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
        _uiMgr = obj.GetComponent<C_UiEventMgr>();
        _photonView = obj.GetComponent<PhotonView>();

        _audioSource = GetComponent<AudioSource>();
        _weaponAttribute = GetComponent<WeaponAttribute>();
        
    }

    public override bool Listener() {

        if (_velocity.Dreload)
        {
            if (_weaponAttribute.runtimeMag < _weaponAttribute.mag)
            {
                if (_velocity.isLocalPlayer)
                {
                    _photonView.RPC("EnterState", PhotonTargets.Others, this._name);
                }
                return true;
            }
        }

        return false;
    }


    public override void Enter()
    {
        base.Enter();
        
        _weaponAttribute.reload = true;
        
        _iKManager.SetAim(false);
        _iKManager.SetHold(false);

        _animator.animator.SetTrigger("reload");

        _audioSource.PlayOneShot(sound);

        //_weaponHandle.locked = true;

    }

    public override void OnUpdate()
    {
        var _anim = _animator.animator;
        AnimatorStateInfo animatorInfo;
        animatorInfo = _anim.GetCurrentAnimatorStateInfo(animLayer);

        _iKManager.SetAim(false);
        _iKManager.SetHold(false);

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

                var ammoMsg = new UiEvent.UiMsgs.Ammo()
                {
                    ammo = _weaponAttribute.runtimeMag + (_weaponAttribute.bore ? 1 : 0),
                    mag = _weaponAttribute.mag
                };
                _uiMgr.SendEvent(ammoMsg);

                this._exitTick = true;
            }
        }
    }


    public override void Exit() {

        base.Exit();

        if (!_velocity.Drun)
        {
            _iKManager.SetAim(true);
            _iKManager.SetHold(true);
        }
        _weaponAttribute.reload = false;
        _weaponHandle.locked = false;
        
    }
}
 