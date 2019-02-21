using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Texture", menuName = ("Datas/Texture"))]
[System.Serializable]
public class TextureData : ScriptableObject
{
    public Texture value;
}
