using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UiEvent;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using System;

public class C_BattleMgr : MonoBehaviour {
    C_Attributes attributes;
    [HideInInspector]
    public C_Velocity velocity;
    C_UiEventMgr uiMgr;
    AudioSource audioSource;
    
    [HideInInspector]
    public PhotonView photonView;
    public MeshRenderer[] friendlyMark;
    public AudioClip killMsgSound;

    public int roomID = 0;
    public string nickName = "";
    public int kill = 0;
    public int dead = 0;
    public int multikill = 0;
    public int assists = 0;
    public int score = 0;
    public int fireCount = 0;
    public int hitCount = 0;
    public int headShot = 0;
    public float demageCount = 0f;
    public Action<int> OnKill;
    [Header("Temp Params")]
    public int tempMultikill = 0;

    private void Awake()
    {
        uiMgr = GetComponent<C_UiEventMgr>();
        velocity = GetComponent<C_Velocity>();
        photonView = GetComponent<PhotonView>();
        attributes = GetComponent<C_Attributes>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        SyncData();
    }
    
    void SyncData()
    {
        if (this.photonView.isMine)
        {
            var p = new Hashtable()
            {
                { "battle", kill + "#" + dead + "#" + assists + "#" + score}
            };
            PhotonNetwork.player.SetCustomProperties(p);
        }
    }

    [PunRPC]
    public void AddKill()
    {
        kill ++;
        tempMultikill ++;
        score += 100;

        Sound.PlayOneShot(this.audioSource, this.killMsgSound);
        uiMgr.SendEvent(new UiEvent.UiMsgs.Kill());
        OnKill?.Invoke(attributes.camp);
        SyncData();
    }


    public void AddDead()
    {
        dead ++;
        if (tempMultikill > multikill)
        {
            multikill = tempMultikill;
        }
        tempMultikill = 0;

        SyncData();
    }

    [PunRPC]
    public void AddKillerMsg(string killer)
    {
        var killerMsg = new UiEvent.UiMsgs.Killer()
        {
            value = killer.Split('#')[0]
        };
        uiMgr.SendEvent(killerMsg);
    }

    [PunRPC]
    public void AddAssists()
    {
        assists ++;
        score += 25;
        uiMgr.SendEvent(new UiEvent.UiMsgs.Assists());
        SyncData();
    }

    [PunRPC]
    public void AddDemage(float demage, bool isHeadShot = false)
    {
        demageCount += demage;
        if (isHeadShot)
        {
            this.headShot++;
        }
    }

    public void AddFire()
    {
        this.fireCount++;
    }

    public void AddHit()
    {
        this.hitCount++;
    }

    public void SetFirendlyMark()
    {
        foreach (var item in friendlyMark)
        {
            item.enabled = Battle.localPlayerCamp == attributes.camp;
        }
    }

    public void SetPlayerEnable(bool isEnable)
    {
        velocity.isActive = isEnable;
        velocity.Reset();
    }

    [PunRPC]
    public void SyncBattleMgr(float time, bool prepare, int score1, int score2)
    {
        Battle.battleMgr.SetSync(time, prepare, score1, score2);
    }

    [PunRPC]
    public void SyncGameOver(int winnerCamp)
    {
        Battle.battleMgr.SetSync(winnerCamp);
    }


}
