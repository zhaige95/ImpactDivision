using UnityEngine;
using System.Collections;
using UnityEngine.Animations;

public class Run : WeaponState
{
    [Header("[Components]")]

    public C_Velocity _velocity;
    public C_IKManager _iKManager;
    public WeaponAttribute _weaponAttribute;
    public PhotonView _photonView;

    public override void Init(GameObject obj)
    {
        _velocity = obj.GetComponent<C_Velocity>();
        _iKManager = obj.GetComponent<C_IKManager>();
        _weaponAttribute = GetComponent<WeaponAttribute>();
        _photonView = obj.GetComponent<PhotonView>();

    }

    public override bool Listener() {

        if (_velocity.Drun && !_velocity.jumping)
        {
            if (!_weaponAttribute.reload)
            {
                if (_velocity.isLocalPlayer)
                {
                    _photonView.RPC("EnterState", PhotonTargets.Others, this._name);
                    Debug.LogWarning("send enter RPC");
                }
                return true;
            }
        }

        return false;
    }


    public override void Enter()
    {
        base.Enter();
        _iKManager.SetAim(false);
        //_weaponAttribute.   
    }

    public override void OnUpdate()
    {
        if (_velocity.isLocalPlayer)
        {
            if (!_velocity.Drun || _velocity.jumping)
            {
                _iKManager.SetAim(true);
                this._exitTick = true;
            }
        }
       
    }
    public override void Exit() {
        if (_velocity.isLocalPlayer)
        {
            _photonView.RPC("ExitState", PhotonTargets.Others, this._name);
            Debug.LogWarning("send Exit RPC");
        }
        base.Exit();
    }
}
 