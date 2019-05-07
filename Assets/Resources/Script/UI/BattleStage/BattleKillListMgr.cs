using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleKillListMgr : MonoBehaviour {
    public GameObject msgItem;
    public RectTransform content;
    public ScrollRect scroll;
    public List<BattleKillMsgItem> battleKillMsgs = new List<BattleKillMsgItem>();

    public void AddKillMsg(string killer, string loser, bool isFriendly = false)
    {
        foreach (var item in battleKillMsgs)
        {
            if (!item.isActive)
            {
                item.Active(killer, loser, isFriendly);
                return;
            }
        }
        var childern = this.transform.GetComponentsInChildren<Transform>();
        var msgObj = GameObject.Instantiate(msgItem, content) as GameObject;
        var msgMgr = msgObj.GetComponent<BattleKillMsgItem>();
        msgMgr.Init(killer, loser, isFriendly);
        battleKillMsgs.Add(msgMgr);
        

    }

    private void FixedUpdate()
    {
        this.scroll.verticalScrollbar.value = 0;
    }

}
