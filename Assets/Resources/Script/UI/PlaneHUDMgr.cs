using UnityEngine;
using System.Collections;
using UiEvent;
using UiMsgs = UiEvent.UiMsgs;

public class PlaneHUDMgr : MonoBehaviour
{
    public Animator animator;
    public Transform tinyHpBarGroup;
    public void Init(C_UiEventMgr eventMgr)
    {

        eventMgr.BindEvent(typeof(UiMsgs.Hit), Hit);
    }

    public void Hit(UiMsg msg)
    {
        animator.SetTrigger("hit");
    }

}
