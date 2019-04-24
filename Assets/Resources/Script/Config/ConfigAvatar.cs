using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName="Avatar_", menuName=("MyConfig/Avatar"))]
[System.Serializable]
public class ConfigAvatar : ScriptableObject {
    public int id = 111;
    public string belong = "符华";
    public string aName = "白夜执事";
    public Texture2D headImage;
    public GameObject IslandModel;

}
