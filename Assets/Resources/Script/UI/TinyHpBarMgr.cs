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
    public Transform tinyHpBarNode;
    public Color friendColor;
    public Color enemyColor;

    public void Init(C_Attributes attributes, C_UiEventMgr eventMgr)
    {
        nameText.color = (attributes.camp == Battle.localPlayerCamp) ? friendColor : enemyColor;
        
        eventMgr.BindEvent(typeof(UiMsgs.Hp), RefreshHp);
    }

    public void RefreshHp(UiMsg msg)
    {
        var hpMsg = msg as UiMsgs.Hp;
        hpBar.fillAmount = hpMsg.hp / hpMsg.hpMax;
    }

}
