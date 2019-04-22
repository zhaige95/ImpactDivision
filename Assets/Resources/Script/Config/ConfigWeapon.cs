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
    public Texture2D cutPicInEquip;
    public Texture2D cutPicInBattle;
    public int mag;
    public float damage;
    public float fireSpeed;
    public float spread = 20f;
    public float aimSpread = 20f;
    public float crouchSpreadRate = 0.5f;
    public float recoilX = 3f;
    public float recoilY = 1f;
    public GameObject model;
}
