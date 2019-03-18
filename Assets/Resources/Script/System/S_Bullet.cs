using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class S_Bullet : ComponentSystem {
	struct Group{
		public C_Bullet _Bullet;
        public Transform _Transform;
	}
    
	protected override void OnUpdate()
	{
		foreach (var e in GetEntities<Group>())
		{
            var _bullet = e._Bullet;
            if (_bullet.isActive)
            {
                var _transform = e._Transform;

                _bullet.timer.Update();

                if (Physics.Raycast(_transform.position, _transform.forward, out _bullet.hit, _bullet.speed * Time.deltaTime, _bullet.layerMask))
                {
                    var hitInfo = _bullet.hit;
                    var hitTag = hitInfo.collider.tag;
                    var _attackListener = hitInfo.collider.gameObject.GetComponentInParent<C_AttackListener>();

                    if (_attackListener != null)
                    {
                        // Add impact effect
                        if (Aspect.IsAvatarTags(hitTag))
                        {
                            Debug.Log(hitTag);
                            var attack = _bullet.attack;
                            // Modify demage by different body part
                            attack.demage = _bullet.attack.demage * AvatarTagsData.demageRate[hitTag];

                            // Set if head shot
                            attack.headShot = AvatarTagsData.IsHead(hitTag);

                            // Add blood effect
                            Effect.AddEffect(_attackListener.hitEffect, hitInfo);

                            // Add attack info in attackListener component
                            _attackListener.attackList.Add(_bullet.attack);
                        }
                    }
                    else
                    {
                        Effect.AddEffect(_bullet.impactEffect, hitInfo);
                    }
                    this.CloseBullet(_bullet, _transform);
                }
                // -------------------------------------------
                _transform.Translate(Vector3.forward * _bullet.speed * Time.deltaTime);
                if (!_bullet.timer.isRunning)
                {
                    this.CloseBullet(_bullet, _transform);
                }
            }
        }
    }
	
    public void CloseBullet(C_Bullet _bullet, Transform _trans)
    {
        _bullet.isActive = false;
        _trans.localScale = Vector3.zero;
    }

}
