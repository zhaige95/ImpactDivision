using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UiEvent;

public class C_BattleMgr : MonoBehaviour {
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
    
    [Header("Temp Params")]
    public int tempMultikill = 0;

    private void Awake()
    {
        attributes = GetComponent<C_Attributes>();
        velocity = GetComponent<C_Velocity>();
        uiMgr = GetComponent<C_UiEventMgr>();
        photonView = GetComponent<PhotonView>();
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
    }


    public void AddDead()
    {
        dead ++;
        if (tempMultikill > miltikill)
        {
            miltikill = tempMultikill;
        }
        tempMultikill = 0;

    }

    [PunRPC]
    public void AddAssists()
    {
        assists ++;
        score += 25;
        uiMgr.SendEvent(new UiEvent.UiMsgs.Assists());
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
    }


}
