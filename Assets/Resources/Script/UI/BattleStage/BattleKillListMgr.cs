using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleKillListMgr : MonoBehaviour {
    public GameObject msgFriendly;
    public GameObject msgEnemy;

    public List<GameObject> msgList = new List<GameObject>();
    public int maxMsgCount = 6;

    public void AddKillMsg(string killer, string loser, bool isEnemy = false)
    {
        if (msgList.Count >= maxMsgCount)
        {
            Object.Destroy(msgList[0]);
        }
        var msgObj = GameObject.Instantiate(isEnemy ? msgEnemy : msgFriendly, this.transform);
        msgObj.GetComponent<BattleKillMsgItem>().Init(killer, loser);
        msgList.Add(msgObj);

    }

    private void Update()
    {
        if (Input.GetKeyDown("j"))
        {
            this.AddKillMsg("如此良人何", "靶场机器人", false);
        }
    }
}
