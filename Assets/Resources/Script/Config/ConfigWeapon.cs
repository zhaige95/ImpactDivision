using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

[CreateAssetMenu(fileName="weapon", menuName=("MyConfig/Weapon"))]
[System.Serializable]
public class ConfigWeapon : ScriptableObject {
    
	public int id;
	public GameObject model;
    public GameObject originalModel;
}
