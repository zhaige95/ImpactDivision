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
        Debug.Log("spread ui process");
    }


}
