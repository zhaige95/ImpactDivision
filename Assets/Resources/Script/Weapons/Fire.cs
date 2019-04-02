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
    public PhotonView _photonView;
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

    public Vector3 targetPoint = new Vector3();
    Vector3 startPoint = new Vector3();
    bool visable = true;

    private RaycastHit hitInfo;
    
    public override void Init(GameObject obj)
    {
        _uiMgr = obj.GetComponent<C_UiEventMgr>();
        _camera = obj.GetComponent<C_Camera>();
        _velocity = obj.GetComponent<C_Velocity>();
        _iKManager = obj.GetComponent<C_IKManager>();
        _photonView = obj.GetComponent<PhotonView>();
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

    public override void Enter()
    {
        base.Enter();
        _velocity.Drun = false;
        _iKManager.SetAim(true);

        if (_velocity.isLocalPlayer)
        {
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
                    startPoint = _weaponHandle.shootPoint.position;
                    targetPoint = OcclusionPoint.position;
                    visable = false;
                    
                }
                else
                {
                    // 激活枪口火光
                    muzzleFlash.SetActive(false);
                    muzzleFlash.SetActive(true);

                    // 弹道后坐力，扩散
                    if (_velocity.aiming)
                    {
                        var hitPos = _camera.GetAimPoint();

                        startPoint = _weaponHandle.shootPoint.position;
                        targetPoint = hitPos;
                        visable = true;
                        
                    }
                    else
                    {
                        float range = _weaponAttribute.spread;
                        Vector2 offset = new Vector2(Random.Range(-range, range), Random.Range(-range, range));

                        startPoint = _weaponHandle.shootPoint.position;
                        targetPoint = _camera.GetAimPoint(offset);
                        visable = true;
                        
                    }
                }
                Effect.AddBullet(
                    bullet, new Attack()
                    {
                        source = _velocity.gameObject,
                        demage = _weaponAttribute.damage,
                        sourcePosition = startPoint,
                    },
                    startPoint,
                    targetPoint,
                    _attributes.camp,
                    visable
                 );

                _photonView.RPC("NetworkFire", PhotonTargets.Others, targetPoint);

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
        else
        {
            Sound.PlayOneShot(_audio, sound);
            _iKManager.aimIK.solver.axis = fireAxis;

            if (Physics.Raycast(OcclusionPoint.position, OcclusionPoint.forward, out hitInfo, OcclusionSensorDistance, occlusionLayer.layerMask))
            {
                startPoint = OcclusionPoint.position;
                visable = false;
            }
            else
            {
                // 激活枪口火光
                muzzleFlash.SetActive(false);
                muzzleFlash.SetActive(true);
                startPoint = _weaponHandle.shootPoint.position;
            }
            
            Effect.AddBullet(
                bullet, new Attack()
                {
                    source = _velocity.gameObject,
                    demage = _weaponAttribute.damage,
                    sourcePosition = startPoint,
                },
                startPoint,
                targetPoint,
                _attributes.camp,
                visable
            );
        }
        
    }

    public override void OnUpdate()
    {
        if (_velocity.isLocalPlayer)
        {
            timer.FixedUpdate();

            if (!timer.isRunning)
            {
                this._exitTick = true;
            }
        }
        else
        {
            this._exitTick = true;
        }
    }
    public override void Exit() {

        base.Exit();
    }
}
 