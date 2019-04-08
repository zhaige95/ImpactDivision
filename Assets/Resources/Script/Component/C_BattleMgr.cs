using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_BattleMgr : MonoBehaviour {
    public C_Attributes attributes;
    public C_Velocity velocity;
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

    }

    [PunRPC]
    public void AddKill()
    {
        kill ++;
        tempMultikill ++;
        score += 100;

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
    }

    [PunRPC]
    public void AddDemage(float demage)
    {
        demageCount += demage;
    }

    public void Recover()
    {
        var t = Battle.bornMgr.GetPoint(attributes.camp);
        this.transform.SetPositionAndRotation(t.position, t.rotation);
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
