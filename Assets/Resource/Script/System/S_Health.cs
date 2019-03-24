using Unity.Entities;
using UnityEngine;

public class S_Health : ComponentSystem {
	struct Group{
        public C_Velocity _Velocity;
		public C_AttackListener _AttackListener;
        public C_Attributes _Attributes;
        public C_Animator _Animator;
        public C_IKManager IKManager;
        public C_WeaponHandle _WeaponHandle;
    }

	protected override void OnUpdate()
	{
		foreach (var e in GetEntities<Group>())
		{
            if (Input.GetKeyDown("e"))
            {
                e._Attributes.HP = 0;
            }
            if (Input.GetKeyDown("f"))
            {
                Revive(e);
            }

            if (e._Attributes.HP <= 0 && e._AttackListener.isActive)
            {
                //var _animator = e._Animator.animator;
                //_animator.SetLayerWeight(8,1);
                //_animator.ResetTrigger("injured");
                //_animator.SetTrigger("injured");

                
                e._AttackListener.isActive = false;
                e._Attributes.isDead = true;
                e._Animator.animator.enabled = false;
                e.IKManager.SetDead(true);
                e._Velocity.isActive = false;
                e._Velocity.Reset();
                e._Velocity.Dmouse_x = 0.2f;
                e._WeaponHandle.Reset();
     
            }
        }
    }

    void Revive(Group e)
    {
        //var _animator = e._Animator.animator;
        //_animator.SetLayerWeight(8, 0);
        //_animator.ResetTrigger("injured");
        //_animator.SetTrigger("injured");

        e._Attributes.HP = e._Attributes.HPMax;
        e._AttackListener.isActive = true;
        e._AttackListener.Reset();
        e._Attributes.isDead = false;
        e._Animator.animator.enabled = true;
        e.IKManager.SetDead(false);
        e._Velocity.isActive = true;
    }
}
