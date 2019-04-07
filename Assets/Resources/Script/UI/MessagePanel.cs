using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MessagePanel : Photon.PunBehaviour, IPointerEnterHandler, IPointerExitHandler{
    public ScrollRect scroll;
    public Transform content;
    public GameObject msgText;
    bool reflashTrigger = false;
    Timer timer = new Timer();
    public float shrinkTime = 3f;
    public bool shrinked = false;
    public MsgPanelEvent mouseEnterEvent;
    public MsgPanelEvent mouseExitEvent;
    public MsgPanelEvent onShrink;
    // Use this for initialization
    void Start () {
        this.timer.Enter(this.shrinkTime);
    }
	
	// Update is called once per frame
	void Update () {
        if (this.reflashTrigger && this.scroll.verticalScrollbar.value != 0)
        {
            this.scroll.verticalScrollbar.value = 0;
            this.reflashTrigger = false;
        }
	}

    private void FixedUpdate()
    {
        this.timer.FixedUpdate();
        if (this.timer.isRunning && !shrinked)
        {
            onShrink.Invoke();
            shrinked = true;
        }
    }

    void AddMsg(string msg)
    {
        GameObject msgObj = GameObject.Instantiate(msgText, content);
        msgObj.GetComponent<Text>().text = msg;
        this.reflashTrigger = true;
    }

    void AddMsg(string msg, string colorValue)
    {
        AddMsg("<color=#" + colorValue + ">" + msg + "</color>");
    }
    
    public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
    {
        this.AddMsg("特工 <color=#ffcf69>[" + newPlayer.NickName + "]</color> 加入战场");
    }

    public override void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
    {
        this.AddMsg("特工 <color=#ffcf69>[" + otherPlayer.NickName + "]</color> 离开了战场");
    }

    public override void OnConnectedToPhoton()
    {
        this.AddMsg("特工 <color=#ffcf69>[" + Battle.playerBasicSave.playerName + "]</color> 欢迎游玩全境崩坏！");
        this.AddMsg("已连接天命作战中心");
    }

    public override void OnDisconnectedFromPhoton()
    {
        this.AddMsg("与天命作战中心的连接断开！", "FF514A");
    }

    public override void OnJoinedLobby()
    {

    }
    
    public override void OnJoinedRoom()
    {
        this.AddMsg("已进入小队");
    }

    public override void OnLeftRoom()
    {
        this.AddMsg("已离开小队");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseEnterEvent.Invoke();
        shrinked = false;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouseExitEvent.Invoke();
        this.timer.Enter(this.shrinkTime);
    }
}

[Serializable]
public class MsgPanelEvent : UnityEvent { }
