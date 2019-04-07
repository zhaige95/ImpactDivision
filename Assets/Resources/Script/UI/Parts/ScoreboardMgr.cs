using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreboardMgr : MonoBehaviour {
    public List<ScoreboardItem> topPanel;
    public List<ScoreboardItem> bottomPanel;

    public bool active = false;
    private void Awake()
    {
        Battle.scoreboardMgr = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown("tab"))
        {
            active = true;
            Battle.ReflashScoreboard();
            this.transform.localScale =  Vector3.one;

        }
        else if(Input.GetKeyUp("tab"))
        {
            active = false;
            this.transform.localScale = Vector3.zero;
        }


    }

}
