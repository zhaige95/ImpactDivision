using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "BattleModel_", menuName = ("MyConfig/Battle"))]
[System.Serializable]
public class ConfigBattle : ScriptableObject {
    public float prepareTime = 15f;
    public int targetKill = 50;
    public float outTime = 1800f;
}
