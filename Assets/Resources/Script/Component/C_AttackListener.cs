using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_AttackListener : MonoBehaviour {

    [Header("Components")]
    public PhotonView photonView;
    public AudioSource audioSource;
    public C_Velocity velocity;
    [Header("Properties")]
    public bool isActive = true;
    public AudioClip killSound;
    public AudioClip[] beAttackedSounds;
    public AudioClip[] hitFeedBackSounds;
    public GameObject hitEffect;
    public List<Attack> attackList = new List<Attack>();
    public Dictionary<string, GameObject> sourceList = new Dictionary<string, GameObject>(); 
    public int injuredAnimLayer = 8;
    
    
    public void Reset()
    {
        attackList.Clear();
    }

    public void PlayBeAttackedSound()
    {
        if (velocity.isLocalPlayer)
        {
            Sound.PlayOneShot(audioSource, beAttackedSounds);
        }
    }
}
