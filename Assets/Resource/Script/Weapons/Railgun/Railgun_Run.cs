using UnityEngine;
using System.Collections;
using UnityEngine.Animations;

public class Railgun_Run : WeaponState
{
    [Header("[Components]")]
    public C_Velocity _velocity;
    public C_IKManager _iKManager;
    public WeaponAttribute _weaponAttribute;

    public override void Init(GameObject obj)
    {
        _velocity = obj.GetComponent<C_Velocity>();
        _iKManager = obj.GetComponent<C_IKManager>();
        _weaponAttribute = GetComponent<WeaponAttribute>();

    }

    public override bool Listener() {

        if (_velocity.Drun && !_velocity.jumping)
        {
            if (!_weaponAttribute.reload)
            {
                return true;
            }
        }

        return false;
    }


    public override void Enter() {
        _iKManager.SetAim(false);
        _iKManager.SetHold(false);
    }

    public override void OnUpdate()
    {
        
        if (!_velocity.Drun || _velocity.jumping)
        {
            this._exitTick = true;
        }
    }
    public override void Exit()
    {
        _iKManager.SetAim(true);
        _iKManager.SetHold(true);
    }
}
 