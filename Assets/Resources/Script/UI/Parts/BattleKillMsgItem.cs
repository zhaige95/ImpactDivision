using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleKillMsgItem : MonoBehaviour {

    public bool isActive = true;
    public Text msgText;
    public Timer timer = new Timer();
    public float dieTime = 5f;
    public float originalHeight = 40f;
    public Color friendlyColor;
    public Color EnemyColor;
    RectTransform rectTransform;

    private void Awake()
    {
        this.rectTransform = GetComponent<RectTransform>();
    }

    public void Init(string killer, string loser, bool isFriendly)
    {
        this.msgText.text = killer.Split('#')[0] + "→" + loser.Split('#')[0];
        this.msgText.color = isFriendly ? this.friendlyColor : this.EnemyColor;
        this.timer.Enter(dieTime);
    }

    public void Active(bool ac)
    {
        this.rectTransform.sizeDelta = new Vector2(this.rectTransform.rect.width, ac ? originalHeight : 0);
        this.isActive = ac;
    }
    public void Active(string killer, string loser, bool isFriendly)
    {
        this.Active(true);
        this.Init(killer, loser, isFriendly);
        this.transform.SetAsLastSibling();
    }
}
