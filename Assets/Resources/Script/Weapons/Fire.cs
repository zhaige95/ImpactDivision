using UnityEngine;
using System.Collections;
using UnityEngine.Animations;
using Data;
using UiEvent;

public class Fire : WeaponState
{
    [Header("[Components]")]
    C_Camera _camera;
    C_Velocity _velocity;
    PhotonView _photonView;
    C_IKManager _iKManager;
    C_UiEventMgr _uiMgr;
    C_Attributes _attributes;
    C_WeaponHandle _weaponHandle;
    C_BattleMgr _battleMgr;

    WeaponAttribute _weaponAttribute;
    AudioSource _audio;

    [Header("[Extra Properties]")]
    public TriggerType triggerType;
    public Timer timer;
    public float spreadFroce = 0.2f;
    public Vector3 fireAxis;
    public AudioClip sound;
    public float bulletVisibleDistence;
    public GameObject bullet;
    public GameObject muzzleFlash;
    public float OcclusionSensorDistance = 2f;
    public ConfigLayer occlusionLayer;

    [HideInInspector]
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
        _battleMgr = obj.GetComponent<C_BattleMgr>();

        _audio = GetComponent<AudioSource>();
        _weaponAttribute = GetComponent<WeaponAttribute>();

        timer = new Timer();

    }

    public override bool Listener() {
        if (_weaponAttribute.ready && !_weaponAttribute.reload)
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

                Transform OcclusionPoint = _weaponHandle.OcclusionPoint;

                if (Physics.Raycast(OcclusionPoint.position, OcclusionPoint.forward, out hitInfo, OcclusionSensorDistance, occlusionLayer.layerMask))
                {
                    startPoint = OcclusionPoint.position;
                    targetPoint = hitInfo.point;
                    visable = false;

                }
                else
                {
                    // 激活枪口火光
                    muzzleFlash.SetActive(false);
                    muzzleFlash.SetActive(true);

                    // 弹道扩散
                    if (_velocity.aiming)
                    {
                        float range = _weaponAttribute.runingSpread * 0.5f * Battle.relativeRate;
                        _weaponAttribute.runingSpread += this.spreadFroce;
                        _weaponAttribute.runingSpread = Mathf.Clamp(_weaponAttribute.runingSpread, 0f, _weaponAttribute.aimSpread);
                        
                        Vector2 offset = new Vector2(Random.Range(-range, range), Random.Range(-range, range));

                        startPoint = _weaponHandle.shootPoint.position;
                        targetPoint = _camera.GetAimPoint(offset);

                        SendSpreadMsg();
                    }
                    else
                    {
                        float range = _weaponAttribute.spread * (_velocity.crouch ? _weaponAttribute.crouchSpreadRate * 0.5f : 0.5f) * Battle.relativeRate;
                        Vector2 offset = new Vector2(Random.Range(-range, range), Random.Range(-range, range));

                        startPoint = _weaponHandle.shootPoint.position;
                        targetPoint = _camera.GetAimPoint(offset);
                        SendSpreadMsg(_weaponAttribute.spread * (_velocity.crouch ? _weaponAttribute.crouchSpreadRate : 1f) + 20f);
                    }
                    visable = Vector3.Distance(startPoint, targetPoint) >= bulletVisibleDistence;
                }

                Effect.AddBullet(
                    bullet, new Attack()
                    {
                        source = _battleMgr,
                        demage = _weaponAttribute.damage,
                    },
                    startPoint,
                    targetPoint,
                    _attributes.camp,
                    visable
                 );

                _weaponAttribute.OnFire?.Invoke();

                _photonView.RPC("NetworkFire", PhotonTargets.Others, _weaponAttribute.index, targetPoint);

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

            // 发送ui消息
            SendUiMsgs();

            _battleMgr.AddFire();

            _camera.mainCamera.fieldOfView += 0.25f;

        }
        else
        {
            Sound.PlayOneShot(_audio, sound);
            _iKManager.aimIK.solver.axis = fireAxis;

            Transform OcclusionPoint = _weaponHandle.OcclusionPoint;
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
                    source = _battleMgr,
                    demage = 0
                },
                startPoint,
                targetPoint,
                _attributes.camp,
                visable,
                false
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
        SendSpreadMsg();
    }

    void SendSpreadMsg()
    {
        SendSpreadMsg(_velocity.Daim ? _weaponAttribute.runingSpread + 10f : _weaponAttribute.spread * (_velocity.crouch ? _weaponAttribute.crouchSpreadRate : 1f) + 10f);
    }

    void SendSpreadMsg(float v)
    {
        var spreadMsg = new UiEvent.UiMsgs.Spread()
        {
            value = v
        };
        _uiMgr.SendEvent(spreadMsg);
    }

    void SendUiMsgs()
    {
        var ammoMsg = new UiEvent.UiMsgs.Ammo()
        {
            ammo = _weaponAttribute.runtimeMag + (_weaponAttribute.bore ? 1 : 0),
            mag = _weaponAttribute.mag
        };
        _uiMgr.SendEvent(ammoMsg);
    }
}
 