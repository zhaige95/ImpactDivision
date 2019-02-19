using UnityEngine;
using System.Collections;
using UnityEngine.Animations;

public class ASR_Reload : WeaponState
{
    [Header("[Extra Properties]")]
    public C_Animator _animator;
    public C_Velocity _velocity;
    public C_IKManager _iKManager;
    public C_WeaponHandle _weaponHandle;

    public CS_StateMgr _stateMgr;

    public AudioSource _audioSource;
    public WeaponData _weaponData;

    public AudioClip[] sounds;

    public float reloadTime;
    public Timer timer;
    public int process = 1;
    public bool hasMag;
    public int weaponType;

    public override void Init(GameObject obj)
    {
        _animator = obj.GetComponent<C_Animator>();
        _velocity = obj.GetComponent<C_Velocity>();
        _iKManager = obj.GetComponent<C_IKManager>();
        _weaponHandle = obj.GetComponent<C_WeaponHandle>();

        _stateMgr = obj.GetComponent<CS_StateMgr>();

        _audioSource = GetComponent<AudioSource>();
        _weaponData = GetComponent<WeaponData>();

        timer = new Timer();
    }

    public override bool Listener() {

        if (_weaponData.runtimeMag < _weaponData.mag)
        {
            if (_velocity.Dreload)
            {
                return true;
            }
        }

        return false;
    }

    public override void Enter() {
        
        _stateMgr.PauseState("aim", true);
        _stateMgr.PauseState("cambat", true);

        process = 1;
        hasMag = (int)_weaponData.hasMag == 1;
        weaponType = (int)_weaponData.wtype;
        if (hasMag)
        {
            _animator.animator.SetTrigger("reload");
            _iKManager.SetAim(false);
            _iKManager.SetHold(false);
            _audioSource.PlayOneShot(sounds[0]);

        }
        else
        {
            timer.Enter(reloadTime);
        }

    }

    public override void OnUpdate()
    {
        var _anim = _animator.animator;



        if (hasMag)
        {
            AnimatorStateInfo animatorInfo;
            animatorInfo = _anim.GetCurrentAnimatorStateInfo(2);
            
            if (animatorInfo.IsTag("reload"))
            {
                if (process == 1)
                {
                    //if (animatorInfo.normalizedTime >= 0.2f)
                    //{
                    _iKManager.SetHold(false);
                    GameObject Mag = _weaponData.magObj;
                    Mag.transform.parent = null;
                    Mag.GetComponent<BoxCollider>().isTrigger = false;
                    Mag.GetComponent<Rigidbody>().useGravity = true;
                    Mag.GetComponent<Rigidbody>().isKinematic = false;

                    Mag.GetComponent<ParentConstraint>().constraintActive = false;

                    process = 2;
                    //}
                }
                else if (process == 2)
                {
                    if (animatorInfo.normalizedTime >= 0.2f)
                    {
                        // Create new mag
                        GameObject newMag = GameObject.Instantiate(_weaponData.magPrefab, _weaponData.transform);

                        _weaponData.magObj.AddComponent<DestoryTimer>();
                        ParentConstraint parentConstraint = newMag.GetComponent<ParentConstraint>();
                        parentConstraint.constraintActive = true;
                        parentConstraint.SetSource(0, new ConstraintSource()
                        {
                            sourceTransform = _weaponHandle.leftHand,
                            weight = 1
                        });
                        parentConstraint.weight = 1;
                        _weaponData.magObj = newMag;

                        process = 3;
                    }
                }
                else if (process == 3)
                {
                    if (animatorInfo.normalizedTime >= 0.5f)
                    {
                        _iKManager.SetHoldTarget(_weaponData.magPoint);
                        _iKManager.SetHold(true);
                        GameObject Mag = _weaponData.magObj;
                        Mag.GetComponent<ParentConstraint>().weight = 0;
                        Mag.GetComponent<ParentConstraint>().constraintActive = false;
                        Mag.transform.localPosition = Vector3.zero;
                        Mag.transform.localEulerAngles = Vector3.zero;
                        _audioSource.PlayOneShot(sounds[1]);
                        process = 4;
                    }
                }
                else if (process == 4)
                {
                    if (animatorInfo.normalizedTime >= 0.7f)
                    {
                        _iKManager.SetHold(false);
                        process = 5;
                    }
                }
                else if (process == 5)
                {
                    if (animatorInfo.normalizedTime >= 0.9f)
                    {
                        _iKManager.SetHoldTarget(_weaponData.holdPoint);
                        _iKManager.SetHold(true);
                        _weaponData.runtimeMag = _weaponData.mag;
                        this._exitTick = true;
                        process = 6;
                    }
                }

            }
        }
        else
        {
            timer.Update();
            if (!timer.isRunning)
            {
                _audioSource.PlayOneShot(_weaponData.soundReload);
                this._exitTick = true;
            }
        }
    }
    public override void Exit() {
        
        GameObject Mag = _weaponData.magObj;
        Mag.GetComponent<BoxCollider>().isTrigger = true;
        Mag.GetComponent<Rigidbody>().useGravity = false;
        Mag.GetComponent<Rigidbody>().isKinematic = true;
        Mag.transform.parent = _weaponData.transform;
        Mag.GetComponent<ParentConstraint>().weight = 0;
        Mag.GetComponent<ParentConstraint>().constraintActive = false;
        Mag.transform.localPosition = Vector3.zero;
        Mag.transform.localEulerAngles = Vector3.zero;
        
        _stateMgr.PauseState("aim", false);
        _stateMgr.PauseState("cambat", false);
    }
}
 