using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName="Avatar_", menuName=("MyConfig/Avatar"))]
[System.Serializable]
public class ConfigAvatar : ScriptableObject {
	public GameObject model;
	public RuntimeAnimatorController animator;
	public float speed = 4f;
	public float jumpForce = 5.2f;
	public float rate = 1.5f;
	public string defaultState = "Move";
	public List<string> states;
	// 存放组件的数组
	public List<string> components;
}
