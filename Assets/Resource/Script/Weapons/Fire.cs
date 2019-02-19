using UnityEngine;
using System.Collections;
using UnityEngine.Animations;
using Data;

public class Fire : WeaponState
{
    [Header("[Components]")]
    public C_Camera _camera;
    public C_Velocity _velocity;
    public C_IKManager _iKManager;
    public C_Attributes _attributes;
    public C_WeaponHandle _weaponHandle;
    public WeaponAttribute _weaponAttribute;
    public AudioSource _audio;

    [Header("[Extra Properties]")]
    public TriggerType triggerType;
    public Timer timer;
    public Vector3 fireAxis;
    public AudioClip sound;
    public GameObject bullet;
    public GameObject muzzleFlash;

    public override void Init(GameObject obj)
    {
        _camera = obj.GetComponent<C_Camera>();
        _velocity = obj.GetComponent<C_Velocity>();
        _iKManager = obj.GetComponent<C_IKManager>();
        _attributes = obj.GetComponent<C_Attributes>();
        _weaponHandle = obj.GetComponent<C_WeaponHandle>();

        _audio = GetComponent<AudioSource>();
        _weaponAttribute = GetComponent<WeaponAttribute>();

        timer = new Timer();

    }

    public override bool Listener() {
        if (_weaponAttribute.ready)
        {
            if ((int)triggerType == 1)
            {
                if (_velocity.DfirePressed)
                {
                    return true;
                }
            }
            else if ((int)triggerType == 2)
            {
                if (_velocity.DfireHold)
                {
                    return true;
                }
            }
        }
        

        return false;
    }

    public override void Enter() {

        _velocity.Drun = false;

        _iKManager.SetAim(true);

        if (_weaponAttribute.bore)
        {
           
            Sound.PlayOneShot(_audio, sound);
            _iKManager.aimIK.solver.axis = fireAxis;
            muzzleFlash.SetActive(false);
            muzzleFlash.SetActive(true);

            // 后坐力
            if (_velocity.aiming)
            {
                Effect.AddBullet(
                    bullet, new Attack()
                    {
                        source = _velocity.gameObject,
                        sourceDriection = _weaponHandle.shootPoint.position,
                        demage = _weaponAttribute.demage,
                    },
                    _weaponHandle.shootPoint.position,
                    _camera.GetAimPoint(),
                    _attributes.camp
                 );
                _camera.forceX = _weaponAttribute.recoilX * 0.1f;
                _camera.forceY = Random.Range(-_weaponAttribute.recoilY * 0.1f, _weaponAttribute.recoilY * 0.1f);

            }
            else
            {
                _camera.forceX = _weaponAttribute.recoilX * 0.05f;

                //float range = _weaponAttribute.spread + (_velocity.crouch ? -_weaponHandle.spreadDec : _weaponHandle.spreadAdd);

                float range = _weaponAttribute.spread;
                Vector2 offset = new Vector2(Random.Range(-range, range), Random.Range(-range, range));

                Effect.AddBullet(
                    bullet, new Attack()
                    {
                        source = _velocity.gameObject,
                        sourceDriection = _weaponHandle.shootPoint.position,
                        demage = _weaponAttribute.demage,
                    },
                    _weaponHandle.shootPoint.position,
                    _camera.GetAimPoint(offset),
                    _attributes.camp
                 );

            }

            if (_weaponAttribute.runtimeMag > 0)
            {
                _weaponAttribute.runtimeMag--;
                _weaponAttribute.bore = true;
            }
            else
            {
                _weaponAttribute.bore = false;
            }

        }
        

        timer.Enter(_weaponAttribute.interval);

    }

    public override void OnUpdate()
    {
        timer.Update();

        if (!timer.isRunning)
        {
            this._exitTick = true;
        }
    }
    public override void Exit() {
        
    }
}
 