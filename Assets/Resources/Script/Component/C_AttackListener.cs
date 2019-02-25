using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_AttackListener : MonoBehaviour {
    public bool isActive = true;
    public AudioClip killSound;
    public AudioClip[] beAttackedSounds;
    public AudioClip[] hitFeedBackSounds;
    public GameObject hitEffect;
    public List<Attack> attackList = new List<Attack>();
    public int injuredAnimLayer = 8;
    public void Reset()
    {
        attackList.Clear();
    }
}
