using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreboardItem : MonoBehaviour {
    public Text playerName;
    public Text kill;
    public Text dead;
    public Text assists;
    public Text point;

    public void Init(string pName, string kill = "0", string dead = "0", string assists = "0", string score = "0")
    {
        this.playerName.text = pName;
        this.kill.text = kill;
        this.dead.text = dead;
        this.assists.text = assists;
        this.point.text = score;
    }

    public void Init(C_BattleMgr mgr)
    {
        var nickName = (mgr.nickName.ToString().Split('#'))[0];
        nickName = mgr.velocity.isLocalPlayer ? ("<color=#ffcf69>" + nickName + "</color>") : nickName;
        this.Init(
            nickName,
            mgr.kill.ToString(),
            mgr.dead.ToString(),
            mgr.assists.ToString(),
            mgr.score.ToString()
        );
    }

    public void Init()
    {
        this.Init("-");
    }
}
