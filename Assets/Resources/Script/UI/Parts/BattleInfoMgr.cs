using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleInfoMgr : Photon.PunBehaviour {
    public Text score1;
    public Text score2;
    public Text target;
    public Text timer;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        
    }
    
    public void SetTimer(float time)
    {
        var mine = (int)(time / 60f);
        var second = (int)(time - mine * 60f);
        timer.text = (mine < 10 ? "0" + mine : mine + "") + ":" + (second < 10 ? "0" + second : second + "");
    }

    public void SetScore(int index, int val)
    {
        SetScore(index, val.ToString());
    }

    public void SetScore(int index, string val)
    {
        if (index == 1)
        {
            score1.text = val;
        }
        else if (index == 2)
        {
            score2.text = val;
        }
    }

    public void SetTarget(int val)
    {
        target.text = val.ToString();
    }

}
