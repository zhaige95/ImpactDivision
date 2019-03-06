using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Animations;
using UiEvent;
using UiMsgs = UiEvent.UiMsgs;

public class TinyHpBarMgr : MonoBehaviour
{
    public Image hpBar;
    public Text nameText;
    public ParentConstraint constraint;

    public void Init(C_Attributes attributes, C_UiEventMgr eventMgr)
    {
        constraint.SetSource(0, new ConstraintSource()
            {
                sourceTransform = attributes.tinyHpBarNode,
                weight = 1
            }
        );
        
        eventMgr.BindEvent(typeof(UiMsgs.Hp), RefreshHp);
    }

    public void RefreshHp(UiMsg msg)
    {
        var hpMsg = msg as UiMsgs.Hp;
        hpBar.fillAmount = hpMsg.hp / hpMsg.hpMax;
    }

}
