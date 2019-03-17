using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;

[CreateAssetMenu(fileName="weapon", menuName=("MyConfig/Weapon"))]
[System.Serializable]
public class ConfigWeapon : ScriptableObject {
    
	public int id;
    public string wname;
    public Data.WeaponType type;
    public Texture2D cutPic;
    public int mag;
    public float damage;
    public float fireSpeed;
    public float spread;
	public GameObject model;
}
