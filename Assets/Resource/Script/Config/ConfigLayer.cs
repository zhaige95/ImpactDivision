using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "LayerMask", menuName = ("MyConfig/LayerMask"))]
[System.Serializable]
public class ConfigLayer : ScriptableObject
{
    public LayerMask layerMask;
}
