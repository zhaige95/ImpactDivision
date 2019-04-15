using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UiEvent;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using System;

public class C_BattleMgr : Photon.PunBehaviour {
    C_Attributes attributes;
    [HideInInspector]
    public C_Velocity velocity;
    C_UiEventMgr uiMgr;
    [HideInInspector]
    public PhotonView photonView;
    public GameObject friendlyMark;
    public int roomID = 0;
    public string nickName = "";
    public int kill = 0;
    public int dead = 0;
    public int miltikill = 0;
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
        attributes = GetComponent<C_Attributes>();
        velocity = GetComponent<C_Velocity>();
        uiMgr = GetComponent<C_UiEventMgr>();
        photonView = GetComponent<PhotonView>();
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
    public void AddKill(bool isHeadShot = false)
    {
        kill ++;
        tempMultikill ++;
        score += 100;

        if (isHeadShot)
        {
            this.headShot++;
        }

        uiMgr.SendEvent(new UiEvent.UiMsgs.Kill());
        OnKill?.Invoke(attributes.camp);
        SyncData();
    }


    public void AddDead()
    {
        dead ++;
        if (tempMultikill > miltikill)
        {
            miltikill = tempMultikill;
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
    public void AddDemage(float demage)
    {
        demageCount += demage;
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
        friendlyMark.SetActive(Battle.localPlayerCamp == attributes.camp);
    }

    public void SetPlayerEnable(bool isEnable)
    {
        velocity.isActive = isEnable;
        velocity.Reset();
    }


}
