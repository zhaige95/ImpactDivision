using UnityEngine;
using System.Collections;
using UiEvent;
using UiMsgs = UiEvent.UiMsgs;

public class PlaneHUDMgr : MonoBehaviour
{
    public Animator animator;

    public void Init(C_UiEventMgr eventMgr)
    {
        eventMgr.BindEvent(typeof(UiMsgs.Hit), Hit);
    }

    public void Hit(UiMsg msg)
    {
        var hitMsg = msg as UiMsgs.Hit;
        animator.SetTrigger(hitMsg.HeadShot ? "hitHead" : "hit");
    }

}
