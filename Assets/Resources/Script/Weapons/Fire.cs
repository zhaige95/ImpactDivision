using UnityEngine;
using System.Collections;
using UnityEngine.Animations;
using Data;
using UiEvent;

public class Fire : WeaponState
{
    [Header("[Components]")]
    public C_Camera _camera;
    public C_Velocity _velocity;
    public C_IKManager _iKManager;
    public C_UiEventMgr _uiMgr;
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
    public Transform OcclusionPoint;
    public float OcclusionSensorDistance;
    public ConfigLayer occlusionLayer;

    private RaycastHit hitInfo;
    
    public override void Init(GameObject obj)
    {
        _uiMgr = obj.GetComponent<C_UiEventMgr>();
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
            if (!_weaponAttribute.bore)
            {
                _velocity.Dreload = true;
                return false;
            }
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
            
            // 后坐力
            if (_velocity.aiming)
            {
                _camera.forceX = _weaponAttribute.recoilX;
                _camera.forceY = Random.Range(-_weaponAttribute.recoilY, _weaponAttribute.recoilY);
            }
            else
            {
                _camera.forceX = _weaponAttribute.recoilX * 0.5f;
            }

            if (Physics.Raycast(OcclusionPoint.position, OcclusionPoint.forward, out hitInfo, OcclusionSensorDistance, occlusionLayer.layerMask))
            {
                Effect.AddBullet(
                    bullet, new Attack()
                    {
                        source = _velocity.gameObject,
                        sourceDriection = _weaponHandle.shootPoint.position,
                        demage = _weaponAttribute.demage,
                        sourcePosition = OcclusionPoint.position,
                    },
                    OcclusionPoint.position,
                    hitInfo.point,
                    _attributes.camp,
                    false
                );
            }
            else
            {
                // 激活枪口火光
                muzzleFlash.SetActive(false);
                muzzleFlash.SetActive(true);

                // 后坐力
                if (_velocity.aiming)
                {
                    var hitPos = _camera.GetAimPoint();
                    Effect.AddBullet(
                        bullet, new Attack()
                        {
                            source = _velocity.gameObject,
                            sourceDriection = _weaponHandle.shootPoint.position,
                            demage = _weaponAttribute.demage,
                            sourcePosition = _weaponHandle.shootPoint.position,
                        },
                        _weaponHandle.shootPoint.position,
                        hitPos,
                        _attributes.camp
                     );
                }
                else
                {
                    float range = _weaponAttribute.spread;
                    Vector2 offset = new Vector2(Random.Range(-range, range), Random.Range(-range, range));

                    Effect.AddBullet(
                        bullet, new Attack()
                        {
                            source = _velocity.gameObject,
                            sourceDriection = _weaponHandle.shootPoint.position,
                            demage = _weaponAttribute.demage,
                            sourcePosition = _weaponHandle.shootPoint.position,
                        },
                        _weaponHandle.shootPoint.position,
                        _camera.GetAimPoint(offset),
                        _attributes.camp
                     );

                }
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
        var ammoMsg = new UiEvent.UiMsgs.Ammo()
        {
            ammo = _weaponAttribute.runtimeMag + (_weaponAttribute.bore ? 1 : 0),
            mag = _weaponAttribute.mag
        };
        _uiMgr.SendEvent(ammoMsg);
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
 