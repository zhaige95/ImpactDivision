using UnityEngine;
using System.Collections;
using RootMotion.FinalIK;

namespace RootMotion.Demos {

	public class VRIKPlatform : MonoBehaviour {

		public VRIK ik;

		private Vector3 lastPosition;
		private Quaternion lastRotation = Quaternion.identity;

		void Start() {
			lastPosition = transform.position;
			lastRotation = transform.rotation;
		}
		
		void Update () {
			// Adding the motion of this Transform to VRIK
			ik.solver.AddPlatformMotion (transform.position - lastPosition, transform.rotation * Quaternion.Inverse(lastRotation), transform.position);

			lastRotation = transform.rotation;
			lastPosition = transform.position;
		}
	}
}
