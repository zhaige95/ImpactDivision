using UnityEngine;
using UnityEditor;
using System.Collections;

namespace RootMotion.FinalIK {
	
	/*
	 * Custom inspector and scene view tools for IKSolverLeg
	 * */
	public class IKSolverLegInspector: IKSolverInspector {
		
		#region Public methods
		
		/*
		 * Draws the custom inspector for IKSolverTrigonometric
		 * */
		public static void AddInspector(SerializedProperty prop, bool editHierarchy, bool showReferences) {
			EditorGUI.indentLevel = 0;
			
			// Bone references
			if (showReferences) {
				EditorGUILayout.Space();
				AddObjectReference(prop.FindPropertyRelative("pelvis.transform"), new GUIContent("Pelvis", "The pelvis (hips)."), editHierarchy, 100);
				AddObjectReference(prop.FindPropertyRelative("thigh.transform"), new GUIContent("Thigh", "The upper leg."), editHierarchy, 100);
				AddObjectReference(prop.FindPropertyRelative("calf.transform"), new GUIContent("Calf", "The lower leg."), editHierarchy, 100);
				AddObjectReference(prop.FindPropertyRelative("foot.transform"), new GUIContent("Foot", "The ankle."), editHierarchy, 100);
				AddObjectReference(prop.FindPropertyRelative("toe.transform"), new GUIContent("Toe", "The first toe bone."), editHierarchy, 100);
				
				EditorGUILayout.Space();
			}
			
			EditorGUILayout.PropertyField(prop.FindPropertyRelative("leg.target"), new GUIContent("Target", "The target Transform. Solver IKPosition will be automatically set to the position of the target."));
			EditorGUILayout.PropertyField(prop.FindPropertyRelative("leg.bendGoal"), new GUIContent("Bend Goal", "If assigned, the knee will be bent in the direction towards this transform."));

			EditorGUILayout.PropertyField(prop.FindPropertyRelative("IKPositionWeight"), new GUIContent("Position Weight", "Solver weight for smooth blending."));
			EditorGUILayout.PropertyField(prop.FindPropertyRelative("IKRotationWeight"), new GUIContent("Rotation Weight", "Weight of last bone's rotation to IKRotation."));
			EditorGUILayout.PropertyField(prop.FindPropertyRelative("leg.bendGoalWeight"), new GUIContent("Bend Goal Weight", "Weight of the bend goal."));

			EditorGUILayout.PropertyField(prop.FindPropertyRelative("leg.swivelOffset"), new GUIContent("Swivel Offset", "Angular offset of the leg's bending direction."));
			EditorGUILayout.PropertyField(prop.FindPropertyRelative("heelOffset"), new GUIContent("Heel Offset", "Offset of the heel in world space."));
		}
		
		/*
		 * Draws the scene view helpers for IKSolverTrigonometric
		 * */
		public static void AddScene(IKSolverLeg solver, Color color, bool modifiable) {
			if (Application.isPlaying && !solver.initiated) return;
			if (!Application.isPlaying && !solver.IsValid()) return;
			
			//float length = Vector3.Distance(solver.bone1.transform.position, solver.bone2.transform.position) + Vector3.Distance(solver.bone2.transform.position, solver.bone3.transform.position);
			//float size = length * 0.05f;
			
			Handles.color = color;
			GUI.color = color;
			
			// Chain lines
			Handles.DrawLine(solver.pelvis.transform.position, solver.thigh.transform.position);
			Handles.DrawLine(solver.thigh.transform.position, solver.calf.transform.position);
			Handles.DrawLine(solver.calf.transform.position, solver.foot.transform.position);
			Handles.DrawLine(solver.foot.transform.position, solver.toe.transform.position);

			// Joints
			Inspector.SphereCap(0, solver.pelvis.transform.position, Quaternion.identity, GetHandleSize(solver.pelvis.transform.position));
			Inspector.SphereCap(0, solver.thigh.transform.position, Quaternion.identity, GetHandleSize(solver.thigh.transform.position));
			Inspector.SphereCap(0, solver.calf.transform.position, Quaternion.identity, GetHandleSize(solver.calf.transform.position));
			Inspector.SphereCap(0, solver.foot.transform.position, Quaternion.identity, GetHandleSize(solver.foot.transform.position));
			Inspector.SphereCap(0, solver.toe.transform.position, Quaternion.identity, GetHandleSize(solver.toe.transform.position));

			if (Application.isPlaying && (solver.IKPositionWeight > 0 || solver.IKRotationWeight > 0)) {
				if (modifiable) {
					Inspector.CubeCap(0, solver.IKPosition, solver.IKRotation, GetHandleSize(solver.IKPosition));
					
					// Manipulating position and rotation
					switch(Tools.current) {
					case Tool.Move:
						if (solver.leg.target == null) solver.IKPosition = Handles.PositionHandle(solver.IKPosition, Quaternion.identity);
						break;
					case Tool.Rotate:
						if (solver.leg.target == null) solver.IKRotation = Handles.RotationHandle(solver.IKRotation, solver.IKPosition);
						break;
					}
				}
				
				// Target
				Handles.color = new Color(color.r, color.g, color.b, color.a * Mathf.Max(solver.IKPositionWeight, solver.IKRotationWeight));
				Handles.DrawLine(solver.toe.transform.position, solver.IKPosition);
			}
			
			Handles.color = Color.white;
			GUI.color = Color.white;
		}
		
		#endregion Public methods
	}
}

