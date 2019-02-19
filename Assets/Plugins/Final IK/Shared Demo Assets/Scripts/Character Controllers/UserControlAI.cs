using UnityEngine;
using System.Collections;

namespace RootMotion.Demos {
	
	/// <summary>
	/// User input for an AI controlled character controller.
	/// </summary>
	public class UserControlAI : UserControlThirdPerson {

		public Transform moveTarget;
		public float stoppingDistance = 0.5f;
		public float stoppingThreshold = 1.5f;

		protected override void Update () {
			float moveSpeed = walkByDefault? 0.5f: 1f;

			Vector3 direction = moveTarget.position - transform.position;
			float distance = direction.magnitude;

			Vector3 normal = transform.up;
			Vector3.OrthoNormalize(ref normal, ref direction);

			float sD = state.move != Vector3.zero? stoppingDistance: stoppingDistance * stoppingThreshold;

			state.move = distance > sD? direction * moveSpeed: Vector3.zero;
		}
	}
}

