using UnityEngine;
using System.Collections;
using UiEvent;
using UiMsgs = UiEvent.UiMsgs;

public class PlaneHUDMgr : MonoBehaviour
{
    public Animator animator;
    public RectTransform aimDrawTrans;
    public float aimDrawDefaultSize = 38f;
    public float aimDrawTargetSize = 38f;
    public float sizeVelocity = 0f;
    public KillMsgMgr killMsgMgr;
    public KillerNamePanel killerNamePanel;
    public BloodScreenMgr bloodScreen;

    // aim point 
    public RectTransform dotTrans;

    private void Awake()
    {
        Battle.planeHUDMgr = this;
    }

    private void FixedUpdate()
    {
        this.sizeVelocity = Mathf.Lerp(this.sizeVelocity, this.aimDrawTargetSize, 0.5f);
        aimDrawTrans.sizeDelta = new Vector2(this.sizeVelocity, this.sizeVelocity);
    }

    public void Init(C_UiEventMgr eventMgr)
    {
        eventMgr.BindEvent(typeof(UiMsgs.Hit), Hit);
        eventMgr.BindEvent(typeof(UiMsgs.Spread), Spread);
        eventMgr.BindEvent(typeof(UiMsgs.Kill), Kill);
        eventMgr.BindEvent(typeof(UiMsgs.Assists), Assists);
        eventMgr.BindEvent(typeof(UiMsgs.Killer), Killer);
        eventMgr.BindEvent(typeof(UiMsgs.Dot), Dot);
        eventMgr.BindEvent(typeof(UiMsgs.Blood), Blood);
    }

    public void Hit(UiMsg msg)
    {
        var hitMsg = msg as UiMsgs.Hit;
        animator.SetTrigger(hitMsg.HeadShot ? "hitHead" : "hit");
    }

    public void Spread(UiMsg msg)
    {
        var spreadMsg = msg as UiMsgs.Spread;

        this.aimDrawTargetSize = this.aimDrawDefaultSize + spreadMsg.value;
        this.sizeVelocity = this.aimDrawTrans.sizeDelta.x;
        
    }

    public void Kill(UiMsg msg)
    {
        killMsgMgr.AddKillMsg();
    }

    public void Assists(UiMsg msg)
    {
        killMsgMgr.AddAssMsg();
    }

    public void Killer(UiMsg msg)
    {
        var killerMsg = msg as UiMsgs.Killer;
        killerNamePanel.Active(killerMsg.value);
    }

    public void Dot(UiMsg msg)
    {
        var dotMsg = msg as UiMsgs.Dot;
        if (dotMsg.visible)
        {
            dotTrans.localScale = Vector3.one;
            dotTrans.position = Battle.localPlayerCamera.WorldToScreenPoint(dotMsg.position);
        }
        else
        {
            dotTrans.localScale = Vector3.zero;
        }
    }

    public void Blood(UiMsg msg)
    {
        this.bloodScreen.Enter();
    }

}
