using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_AttackListener : MonoBehaviour {

    [Header("Components")]
    [HideInInspector]
    public PhotonView photonView;
    AudioSource audioSource;
    C_Velocity velocity;
    C_Attributes attributes;
    [Header("Properties")]
    public bool isActive = true;
    public AudioClip killSound;
    public AudioClip[] beAttackedSounds;
    public AudioClip[] hitFeedBackSounds;
    public GameObject hitEffect;
    public List<Attack> attackList = new List<Attack>();
    public Dictionary<int, C_BattleMgr> sourceList = new Dictionary<int, C_BattleMgr>(); 
    public int injuredAnimLayer = 8;

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
        audioSource = GetComponent<AudioSource>();
        velocity = GetComponent<C_Velocity>();
        attributes = GetComponent<C_Attributes>();
    }

    public void Reset()
    {
        attackList.Clear();
        sourceList.Clear();
    }
    
    public void PlayBeAttackedSound()
    {
        if (velocity.isLocalPlayer)
        {
            Sound.PlayOneShot(audioSource, beAttackedSounds);
        }
    }

    [PunRPC]
    public void AddAttackSource(int sourceID)
    {
        var source = Battle.GetPlayerInfoByRoomID(sourceID, attributes.camp);
        if (source != null)
        {
            if (!sourceList.ContainsKey(sourceID))
            {
                sourceList.Add(sourceID, Battle.GetPlayerInfoByRoomID(sourceID, attributes.camp));
            }
        }
    }
}
