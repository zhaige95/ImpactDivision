using UnityEngine;
using System.Collections;

namespace RootMotion.FinalIK {

	/// <summary>
	/// Calibrates VRIK for the HMD and up to 5 additional trackers.
	/// </summary>
    public static class VRIKCalibrator {

    	/// <summary>
    	/// The settings for VRIK tracker calibration.
    	/// </summary>
        [System.Serializable]
        public class Settings {

        	/// <summary>
			/// Local axis of the HMD facing forward.
        	/// </summary>
            [Tooltip("Local axis of the HMD facing forward.")]
            public Vector3 headTrackerForward = Vector3.forward;

            /// <summary>
			/// Local axis of the HMD facing up.
            /// </summary>
            [Tooltip("Local axis of the HMD facing up.")]
            public Vector3 headTrackerUp = Vector3.up;

            /// <summary>
			/// Local axis of the body tracker towards the player's forward direction.
            /// </summary>
			[Tooltip("Local axis of the body tracker towards the player's forward direction.")]
			public Vector3 bodyTrackerForward = Vector3.forward;

			/// <summary>
			/// Local axis of the body tracker towards the up direction.
			/// </summary>
			[Tooltip("Local axis of the body tracker towards the up direction.")]
			public Vector3 bodyTrackerUp = Vector3.up;

			/// <summary>
			/// Local axis of the hand trackers pointing from the wrist towards the palm.
			/// </summary>
			[Tooltip("Local axis of the hand trackers pointing from the wrist towards the palm.")]
			public Vector3 handTrackerForward = Vector3.forward;

			/// <summary>
			/// Local axis of the hand trackers pointing in the direction of the surface normal of the back of the hand.
			/// </summary>
			[Tooltip("Local axis of the hand trackers pointing in the direction of the surface normal of the back of the hand.")]
			public Vector3 handTrackerUp = Vector3.up;

			/// <summary>
			/// Local axis of the foot trackers towards the player's forward direction.
			/// </summary>
			[Tooltip("Local axis of the foot trackers towards the player's forward direction.")]
			public Vector3 footTrackerForward = Vector3.forward;

			/// <summary>
			/// Local axis of the foot tracker towards the up direction.
			/// </summary>
			[Tooltip("Local axis of the foot tracker towards the up direction.")]
			public Vector3 footTrackerUp = Vector3.up;

            [Space(10f)]
            /// <summary>
			/// Offset of the head bone from the HMD in (headTrackerForward, headTrackerUp) space relative to the head tracker.
            /// </summary>
			[Tooltip("Offset of the head bone from the HMD in (headTrackerForward, headTrackerUp) space relative to the head tracker.")]
			public Vector3 headOffset;

			/// <summary>
			/// Offset of the hand bones from the hand trackers in (handTrackerForward, handTrackerUp) space relative to the hand trackers.
			/// </summary>
			[Tooltip("Offset of the hand bones from the hand trackers in (handTrackerForward, handTrackerUp) space relative to the hand trackers.")]
			public Vector3 handOffset;

			/// <summary>
			/// Forward offset of the foot bones from the foot trackers.
			/// </summary>
			[Tooltip("Forward offset of the foot bones from the foot trackers.")]
			public float footForwardOffset;

			/// <summary>
			/// Inward offset of the foot bones from the foot trackers.
			/// </summary>
			[Tooltip("Inward offset of the foot bones from the foot trackers.")]
			public float footInwardOffset;

			/// <summary>
			/// Used for adjusting foot heading relative to the foot trackers.
			/// </summary>
			[Tooltip("Used for adjusting foot heading relative to the foot trackers.")] [Range(-180f, 180f)]
			public float footHeadingOffset;
        }

        /// <summary>
        /// Calibrates VRIK to the specified trackers using the VRIKTrackerCalibrator.Settings.
        /// </summary>
        /// <param name="ik">Reference to the VRIK component.</param>
        /// <param name="settings">Calibration settings.</param>
        /// <param name="headTracker">The HMD.</param>
		/// <param name="bodyTracker">(Optional) A tracker placed anywhere on the body of the player, preferrably close to the pelvis, on the belt area.</param>
		/// <param name="leftHandTracker">(Optional) A tracker or hand controller device placed anywhere on or in the player's left hand.</param>
		/// <param name="rightHandTracker">(Optional) A tracker or hand controller device placed anywhere on or in the player's right hand.</param>
		/// <param name="leftFootTracker">(Optional) A tracker placed anywhere on the ankle or toes of the player's left leg.</param>
		/// <param name="rightFootTracker">(Optional) A tracker placed anywhere on the ankle or toes of the player's right leg.</param>
        public static void Calibrate(VRIK ik, Settings settings, Transform headTracker, Transform bodyTracker = null, Transform leftHandTracker = null, Transform rightHandTracker = null, Transform leftFootTracker = null, Transform rightFootTracker = null) {
        	if (!ik.solver.initiated) {
        		Debug.LogError("Can not calibrate before VRIK has initiated.");
        		return;
        	}

        	if (headTracker == null) {
        		Debug.LogError("Can not calibrate VRIK without the head tracker.");
        		return;
        	}

        	// Head
        	Transform headTarget = ik.solver.spine.headTarget == null? (new GameObject("Head Target")).transform: ik.solver.spine.headTarget;
        	headTarget.position = headTracker.position + headTracker.rotation * Quaternion.LookRotation(settings.headTrackerForward, settings.headTrackerUp) * settings.headOffset;
        	headTarget.rotation = ik.references.head.rotation;
        	headTarget.parent = headTracker;
        	ik.solver.spine.headTarget = headTarget;

        	// Size
        	float sizeF = headTarget.position.y / ik.references.head.position.y;
        	ik.references.root.localScale = Vector3.one * sizeF;

        	// Root position and rotation
			ik.references.root.position = new Vector3(headTarget.position.x, ik.references.root.position.y, headTarget.position.z);
        	Vector3 headForward = headTracker.rotation * settings.headTrackerForward;
        	headForward.y = 0f;
        	ik.references.root.rotation = Quaternion.LookRotation(headForward);

        	// Body
        	if (bodyTracker != null) {
	        	Transform pelvisTarget = ik.solver.spine.pelvisTarget == null? (new GameObject("Pelvis Target")).transform: ik.solver.spine.pelvisTarget;
	        	pelvisTarget.position = ik.references.pelvis.position;
	        	pelvisTarget.rotation = ik.references.pelvis.rotation;
	        	pelvisTarget.parent = bodyTracker;
	        	ik.solver.spine.pelvisTarget = pelvisTarget;
	        	ik.solver.spine.pelvisPositionWeight = 1f;
	        	ik.solver.spine.pelvisRotationWeight = 1f;

				ik.solver.plantFeet = false;
				ik.solver.spine.neckStiffness = 0f;
				ik.solver.spine.maxRootAngle = 180f;
        	} else if (leftFootTracker != null && rightFootTracker != null) {
        		ik.solver.spine.maxRootAngle = 0f;
        	}

        	// Left Hand
        	if (leftHandTracker != null) {
	        	Transform leftHandTarget = ik.solver.leftArm.target == null? (new GameObject("Left Hand Target")).transform: ik.solver.leftArm.target;
	        	leftHandTarget.position = leftHandTracker.position + leftHandTracker.rotation * Quaternion.LookRotation(settings.handTrackerForward, settings.handTrackerUp) * settings.handOffset;
	        	Vector3 leftHandUp = Vector3.Cross(ik.solver.leftArm.wristToPalmAxis, ik.solver.leftArm.palmToThumbAxis);
	        	leftHandTarget.rotation = QuaTools.MatchRotation(leftHandTracker.rotation * Quaternion.LookRotation(settings.handTrackerForward, settings.handTrackerUp), settings.handTrackerForward, settings.handTrackerUp, ik.solver.leftArm.wristToPalmAxis, leftHandUp);
				leftHandTarget.parent = leftHandTracker;
				ik.solver.leftArm.target = leftHandTarget;
			} else {
				ik.solver.leftArm.positionWeight = 0f;
				ik.solver.leftArm.rotationWeight = 0f;
			}

			// Right Hand
			if (rightHandTracker != null) {
				Transform rightHandTarget = ik.solver.rightArm.target == null? (new GameObject("Right Hand Target")).transform: ik.solver.rightArm.target;
				rightHandTarget.position = rightHandTracker.position + rightHandTracker.rotation * Quaternion.LookRotation(settings.handTrackerForward, settings.handTrackerUp) * settings.handOffset;
	        	Vector3 rightHandUp = -Vector3.Cross(ik.solver.rightArm.wristToPalmAxis, ik.solver.rightArm.palmToThumbAxis);
				rightHandTarget.rotation = QuaTools.MatchRotation(rightHandTracker.rotation * Quaternion.LookRotation(settings.handTrackerForward, settings.handTrackerUp), settings.handTrackerForward, settings.handTrackerUp, ik.solver.rightArm.wristToPalmAxis, rightHandUp);
				rightHandTarget.parent = rightHandTracker;
				ik.solver.rightArm.target = rightHandTarget;
			} else {
				ik.solver.rightArm.positionWeight = 0f;
				ik.solver.rightArm.rotationWeight = 0f;
			}
			 
			// Legs
			if (leftFootTracker != null) CalibrateLeg(settings, leftFootTracker, ik.solver.leftLeg, (ik.references.leftToes != null? ik.references.leftToes: ik.references.leftFoot), ik.references.root.forward, true);
			if (rightFootTracker != null) CalibrateLeg(settings, rightFootTracker, ik.solver.rightLeg, (ik.references.rightToes != null? ik.references.rightToes: ik.references.rightFoot), ik.references.root.forward, false);

			// Root controller
			bool addRootController = bodyTracker != null || (leftFootTracker != null && rightFootTracker != null);
			var rootController = ik.references.root.GetComponent<VRIKRootController>();

        	if (addRootController) {
				if (rootController == null) rootController = ik.references.root.gameObject.AddComponent<VRIKRootController>();
				rootController.pelvisTarget = ik.solver.spine.pelvisTarget;
				rootController.leftFootTarget = ik.solver.leftLeg.target;
				rootController.rightFootTarget = ik.solver.rightLeg.target;
				rootController.Calibrate();
			} else {
				if (rootController != null) GameObject.Destroy(rootController);
			}

			// Additional solver settings
			ik.solver.spine.minHeadHeight = 0f;
			ik.solver.locomotion.weight = bodyTracker == null && leftFootTracker == null && rightFootTracker == null? 1f: 0f;
        }

		private static void CalibrateLeg(Settings settings, Transform tracker, IKSolverVR.Leg leg, Transform lastBone, Vector3 rootForward, bool isLeft) {
			string name = isLeft? "Left": "Right";
			Transform target = leg.target == null? (new GameObject(name + " Foot Target")).transform: leg.target;

			// Space of the tracker heading
			Quaternion trackerSpace = tracker.rotation * Quaternion.LookRotation(settings.footTrackerForward, settings.footTrackerUp);
			Vector3 f = trackerSpace * Vector3.forward;
			f.y = 0f;
			trackerSpace = Quaternion.LookRotation(f);

			// Target position
			float inwardOffset = isLeft? settings.footInwardOffset: -settings.footInwardOffset;
			target.position = tracker.position + trackerSpace * new Vector3(inwardOffset, 0f, settings.footForwardOffset);
			target.position = new Vector3(target.position.x, lastBone.position.y, target.position.z);

			// Target rotation
			target.rotation = lastBone.rotation;

			// Rotate target forward towards tracker forward
			Vector3 footForward = AxisTools.GetAxisVectorToDirection(lastBone, rootForward);
			if (Vector3.Dot(lastBone.rotation * footForward, rootForward) < 0f) footForward = -footForward;
			Vector3 fLocal = Quaternion.Inverse(Quaternion.LookRotation(target.rotation * footForward)) * f;
			float angle = Mathf.Atan2(fLocal.x, fLocal.z) * Mathf.Rad2Deg;
			float headingOffset = isLeft? settings.footHeadingOffset: -settings.footHeadingOffset;
			target.rotation = Quaternion.AngleAxis(angle + headingOffset, Vector3.up) * target.rotation;

			target.parent = tracker;
			leg.target = target;

			leg.positionWeight = 1f;
			leg.rotationWeight = 1f;

			// Bend goal
			Transform bendGoal = leg.bendGoal == null? (new GameObject(name + " Leg Bend Goal")).transform: leg.bendGoal;
			bendGoal.position = lastBone.position + trackerSpace * Vector3.forward + trackerSpace * Vector3.up;// * 0.5f;
			bendGoal.parent = tracker;
			leg.bendGoal = bendGoal;
			leg.bendGoalWeight = 1f;
        }
    }
}
