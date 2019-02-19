using UnityEngine;
using System.Collections;

namespace RootMotion.FinalIK {

	public class VRIKRootController : MonoBehaviour {

		public Transform pelvisTarget;
		public Transform leftFootTarget;
		public Transform rightFootTarget;

		private Vector3 pelvisTargetRight;
		private VRIK ik;

		void Awake() {
			ik = GetComponent<VRIK>();
			ik.solver.OnPreUpdate += OnPreUpdate;
		}

		public void Calibrate() {
			if (pelvisTarget != null) pelvisTargetRight = Quaternion.Inverse(pelvisTarget.rotation) * ik.references.root.right;
		}

		void OnPreUpdate() {
			if (!enabled) return;

			if (pelvisTarget != null) {
				ik.references.root.position = new Vector3(pelvisTarget.position.x, ik.references.root.position.y, pelvisTarget.position.z);

				Vector3 f = Vector3.Cross(pelvisTarget.rotation * pelvisTargetRight, ik.references.root.up);
				f.y = 0f;
				ik.references.root.rotation = Quaternion.LookRotation(f);

				ik.references.pelvis.position = pelvisTarget.position;
				ik.references.pelvis.rotation = pelvisTarget.rotation;
			} else if (leftFootTarget != null && rightFootTarget != null) {
				ik.references.root.position = Vector3.Lerp(leftFootTarget.position, rightFootTarget.position, 0.5f);
			}
		}

		void OnDestroy() {
			if (ik != null) ik.solver.OnPreUpdate -= OnPreUpdate;
		}

	}
}
