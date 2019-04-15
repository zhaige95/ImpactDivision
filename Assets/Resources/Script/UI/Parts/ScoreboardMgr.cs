using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreboardMgr : PanelSwitch
{
    public List<ScoreboardItem> topPanel;
    public List<ScoreboardItem> bottomPanel;

    public bool active = false;
    private void Awake()
    {
        Battle.scoreboardMgr = this;
    }

    public override void SwitchPanel(bool isOpen)
    {
        base.SwitchPanel(isOpen);
        if (isOpen)
        {
            active = true;
            Battle.ReflashScoreboard();
            this.transform.localScale = Vector3.one;
        }
        else
        {
            active = false;
            this.transform.localScale = Vector3.zero;
        }
    }

}
