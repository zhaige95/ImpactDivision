using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleKillMsgItem : MonoBehaviour {

    public Text killerText;
    public Text LoserText;
    public List<ContentSizeFitter> sizeFitters;
    public float dieTime = 5f;

    public void Init(string killer, string loser)
    {
        this.killerText.text = killer;
        this.LoserText.text = loser;

        foreach (var item in sizeFitters)
        {
            killerText.rectTransform.sizeDelta = new Vector2(20, killerText.rectTransform.sizeDelta.y);
            LoserText.rectTransform.sizeDelta = new Vector2(20, LoserText.rectTransform.sizeDelta.y);
            item.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
        }

        Object.Destroy(this.gameObject, dieTime);

    }
}
