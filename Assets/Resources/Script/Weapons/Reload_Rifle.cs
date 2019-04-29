using UnityEngine;
using System.Collections;
using UnityEngine.Animations;
using UiEvent;

public class Reload_Rifle : WeaponState
{
    [Header("[Components]")]
    C_Animator _animator;
    C_Velocity _velocity;
    C_IKManager _iKManager;
    C_WeaponHandle _weaponHandle;
    C_UiEventMgr _uiMgr;
    PhotonView _photonView;

    AudioSource _audioSource;
    WeaponAttribute _weaponAttribute;
    Animator _ownAnimator;

    [Header("[Extra Properties]")]
    public Transform magIK;
    public AudioClip[] sounds;
    public int animLayer = 5;
    public float reloadTime = 1.5f;
    public float magInTime = 0.5f;

    Timer timer;
    int process = 1;

    public override void Init(GameObject obj)
    {
        _animator = obj.GetComponent<C_Animator>();
        _uiMgr = obj.GetComponent<C_UiEventMgr>();
        _velocity = obj.GetComponent<C_Velocity>();
        _iKManager = obj.GetComponent<C_IKManager>();
        _weaponHandle = obj.GetComponent<C_WeaponHandle>();
        _photonView = obj.GetComponent<PhotonView>();

        _ownAnimator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        _weaponAttribute = GetComponent<WeaponAttribute>();

        timer = new Timer();
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

    public override void Enter() {
        base.Enter();

        _weaponAttribute.reload = true;

        timer.Enter(reloadTime);
        process = 1;
        
        _animator.animator.SetTrigger("reload");
        _ownAnimator.SetTrigger("reload");
        _iKManager.SetAim(false);
        _iKManager.SetHoldTarget(magIK);
        //_weaponHandle.locked = true;

    }

    public override void OnUpdate()
    {
        this.timer.Update();
        var _anim = _animator.animator;

        AnimatorStateInfo animatorInfo;
        animatorInfo = _anim.GetCurrentAnimatorStateInfo(animLayer);

        _iKManager.SetAim(false);

        if (process == 1)
        {
            _audioSource.PlayOneShot(sounds[0]);
            process = 2;
        }
        
        if (animatorInfo.IsTag("reload"))
        {
            if (process == 2)
            {
                if (animatorInfo.normalizedTime >= this.magInTime)
                {
                    _audioSource.PlayOneShot(sounds[1]);
                    if (_velocity.isLocalPlayer)
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
                    }
                    process = 3;
                }
            }
            else if (process == 3)
            {
                if (!this.timer.isRunning)
                {
                    this._exitTick = true;
                    process = 4;
                }
            }
        }
    }
    public override void Exit() {
        base.Exit();
        if (process < 4)
        {
            _ownAnimator.SetTrigger("exitReload");
        }
        _iKManager.SetHold(true);
        _iKManager.SetAim(true);
        _weaponAttribute.reload = false;
        _weaponHandle.locked = false;

        _iKManager.SetHoldTarget(_weaponAttribute.holdPoint);
        
    }
}
 