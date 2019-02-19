using Unity.Entities;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class S_Move : ComponentSystem {
	struct Group{
		public C_Velocity _velocity;
        public C_Attributes _Attributes;
        public C_Animator _Animator;
        public C_Camera _Camera;
        public CharacterController _CharacterController;
        public AudioSource _AudioSource;
    }

	protected override void OnUpdate()
	{
		foreach (var e in GetEntities<Group>())
		{
            var _attributes = e._Attributes;

            if (!_attributes.isDead)
            {
                //var _animator = e._Animator.animator;
                //var _velocity = e._velocity;
                //var _characterController = e._CharacterController;
                //var _transform = e._CharacterController.transform;

                //if (_velocity.Dfwd || _velocity.Dbwd || _velocity.Dright || _velocity.Dleft)
                //{
                //    float currentSpeed = _velocity.slow ? _attributes.walkSpeed : _attributes.runSpeed;
                    
                //    _characterController.Move(_transform.forward * currentSpeed * _attributes.rate * _velocity.fwd * Time.deltaTime +
                //                        _transform.right * currentSpeed * _attributes.rate * _velocity.right * Time.deltaTime);
                    
                //    Aspect.RotateToCameraY(e._Camera.Carryer, _transform, 0.5f);

                //    // foot step sound
                //    _attributes.timer.Update();
                    
                //}

                //if (_velocity.jumping)
                //{
                //    _characterController.Move(_transform.up * (_velocity.jumping ? _attributes.jumpForce : 0) * Time.deltaTime);
                //}
                //else
                //{
                //    if (!_attributes.timer.isRunning)
                //    {
                //        Sound.PlayOneShot(e._AudioSource, _attributes.sounds);
                //        _attributes.timer.Enter(_velocity.slow? _attributes.walkSteptime : _attributes.runSteptime);
                //    }
                //}
                //_animator.SetFloat("Dfwd", _velocity.fwd);
                //_animator.SetFloat("Dright", _velocity.right);
                //_characterController.SimpleMove(Vector3.zero);
            }
            
        }
    }
}
