using UnityEngine;
using System.Collections;

public class Shoot : WeaponState
{
    [Header("[Extra Properties]")]
    public C_Camera _camera;
    public C_Velocity _velocity;
    public C_IKManager _iKManager;
    public C_Attributes _attributes;
    public C_WeaponHandle _weaponHandle;

    public AudioSource _audio;
    public WeaponData _weaponData;
    
    public Timer _timer;

    public override void Init(GameObject obj)
    {
        _camera = obj.GetComponent<C_Camera>();
        _velocity = obj.GetComponent<C_Velocity>();
        _iKManager = obj.GetComponent<C_IKManager>();
        _attributes = obj.GetComponent<C_Attributes>();
        _weaponHandle = obj.GetComponent<C_WeaponHandle>();
        
        _audio = GetComponent<AudioSource>();
        _weaponData = GetComponent<WeaponData>();

        _timer = new Timer(_weaponData.interval);
    }

    public override bool Listener() {
        if ((int)_weaponData.triggerType == 1)
        {
            if (_velocity.DfirePressed)
            {
                return true;
            }
        }
        else if ((int)_weaponData.triggerType == 2)
        {
            if (_velocity.DfireHold)
            {
                return true;
            }
        }

        return false;
    }

    public override void Enter()
    {
        _timer.Enter();
        if (_weaponData.runtimeMag > 0)
        {
            Sound.PlayOneShot(_audio, _weaponData.soundFire);
            _iKManager.aimIK.solver.axis = _weaponData.fireAxis;
            _weaponData.muzzleFlash.SetActive(false);
            _weaponData.muzzleFlash.SetActive(true);

            // 后坐力
            if (_velocity.aiming)
            {
                Effect.AddBullet(
                    _weaponData.bullet, new Attack()
                    {
                        source = _velocity.gameObject,
                        sourceDriection = _weaponHandle.shootPoint.position,
                        demage = _weaponData.demage,
                    },
                    _weaponHandle.shootPoint.position,
                    _camera.GetAimPoint(),
                    _attributes.camp
                 );
                _camera.camera_x.Rotate(-Random.Range(0, _weaponData.recoilX), 0, 0);
                _camera.camera_y.Rotate(0, Random.Range(-_weaponData.recoilY, _weaponData.recoilY), 0);
            }
            else
            {
                _camera.camera_x.Rotate(-Random.Range(0, _weaponData.recoilX * 0.5f), 0, 0);

                float range = _weaponData.spread + (_velocity.aiming ? -_weaponHandle.spreadDec : _weaponHandle.spreadAdd);
                Vector2 offset = new Vector2(Random.Range(-range, range), Random.Range(-range, range));
                Effect.AddBullet(
                    _weaponData.bullet, new Attack()
                    {
                        source = _velocity.gameObject,
                        sourceDriection = _weaponHandle.shootPoint.position,
                        demage = _weaponData.demage,
                    },
                    _weaponHandle.shootPoint.position,
                    _camera.GetAimPoint(offset),
                    _attributes.camp
                 );
            }

            _weaponData.runtimeMag--;
        }
    }

    public override void OnUpdate() {
        _timer.Update();
        
        if (!_timer.isRunning)
        {
            this._exitTick = true;
        }
    }

    public override void Exit() {
        
    }
}
 