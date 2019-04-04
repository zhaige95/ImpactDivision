using Unity.Entities;
using UnityEngine;
using UiEvent;

public class S_Health : ComponentSystem {
    struct Group {
        public C_AttackListener _AttackListener;
        public C_Attributes _Attributes;
        public CS_StateMgr _StateMgr;
    }

    protected override void OnUpdate()
    {
        foreach (var e in GetEntities<Group>())
        {
            var _attribute = e._Attributes;

            if (!_attribute.isDead) {

                if (e._Attributes.HP <= 0 && e._AttackListener.isActive)
                {
                    //var _animator = e._Animator.animator;
                    //_animator.SetLayerWeight(8,1);
                    //_animator.ResetTrigger("injured");
                    //_animator.SetTrigger("injured");

                    //_attribute.timer.Enter(_attribute.recoverTime);
                    //e._AttackListener.isActive = false;
                    //e._Attributes.isDead = true;
                    //e._Animator.animator.enabled = false;
                    //e._Velocity.isActive = false;
                    //e._Velocity.Reset();
                    //e._WeaponHandle.Reset();
                    //e.IKManager.SetDead(true);

                    e._StateMgr.EnterState("dead");

                }
            }
        }
    }


    //void Revive(Group e)
    //{
    //    //var _animator = e._Animator.animator;
    //    //_animator.SetLayerWeight(8, 0);
    //    //_animator.ResetTrigger("injured");
    //    //_animator.SetTrigger("injured");
        
    //    // use API replace
    //    e._Attributes.Recover();

    //    e._WeaponHandle.PickWeapon();
    //    e._AttackListener.isActive = true;
    //    e._AttackListener.Reset();
    //    e._Attributes.isDead = false;
    //    e._Animator.animator.enabled = true;
    //    e.IKManager.SetDead(false);
    //    e._Velocity.isActive = true;
    //}
}
