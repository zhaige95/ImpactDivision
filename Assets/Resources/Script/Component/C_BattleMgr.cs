using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_BattleMgr : MonoBehaviour {
    public C_Attributes attributes;
    public int kill;
    public int dead;
    public int miltikill;
    public int assists;
    public int score;
    public int fireCount;
    public int hitCount;
    public int headShot;
    public float demageCount;
    
    [Header("Temp Params")]
    public int tempMultikill = 0;

    private void Awake()
    {
        //    attributes.OnDead = AddDead;
        //    attributes.OnRecover = Recover;
    }

    public void AddKill()
    {
        kill += 1;
        tempMultikill += 1;
        score += 100;

    }

    public void AddDead()
    {
        dead += 1;
        if (tempMultikill > miltikill)
        {
            miltikill = tempMultikill;
        }
        tempMultikill = 0;

    }

    public void AddAssists()
    {
        assists += 1;
        score += 50;
    }

    public void AddDemage(float demage)
    {
        demageCount += demage;
    }

    public void Recover()
    {
        var t = Battle.bornMgr.GetPoint(attributes.camp);
        this.transform.SetPositionAndRotation(t.position, t.rotation);
    }

}
