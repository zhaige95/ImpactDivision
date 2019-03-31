using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using DG.Tweening;

public class S_Velocity : ComponentSystem {
	struct Group{
		public C_Velocity _Velocity;
        public C_Animator _Animator;
	}

	float targetFwd;
	float targetRight;

    float fwd;
    float right;

	protected override void OnUpdate()
	{
		foreach (var e in GetEntities<Group>())
		{
			var _velocity = e._Velocity;
            if (_velocity.isLocalPlayer)
            {
                //_velocity.moving = _velocity.Dfwd ? true : _velocity.Dbwd ? true : _velocity.Dright ? true : _velocity.Dleft;
                _velocity.idle = _velocity.Dfwd ? false : _velocity.Dbwd ? false : _velocity.Dright ? false : _velocity.Dleft ? false : true;

                if (_velocity.idle || _velocity.Daim || _velocity.Dcrouch)
                {
                    _velocity.Drun = false;
                }
                //e._Animator.animator.SetBool("moving", _velocity.moving);

                this.fwd = (_velocity.Dfwd ? 1f : 0) + (_velocity.Dbwd ? -1f : 0);
                this.right = (_velocity.Dright ? 1f : 0) + (_velocity.Dleft ? -1f : 0);

                this.targetFwd = fwd * Mathf.Sqrt(1 - (right * right) * 0.5f);
                this.targetRight = right * Mathf.Sqrt(1 - (fwd * fwd) * 0.5f);

                DOTween.To(() => _velocity.fwd, x => _velocity.fwd = x, this.targetFwd, 0.3f);
                DOTween.To(() => _velocity.right, x => _velocity.right = x, this.targetRight, 0.3f);


                _velocity.mouse = _velocity.Dmouse_x > 0 ? 1f : _velocity.Dmouse_x < 0 ? -1f : 0;
            }
		}
	}
	

}
