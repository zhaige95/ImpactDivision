using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Animations;

public class TinyHpBarMgr : MonoBehaviour
{
    public C_Attributes _attributes;
    public Image hpBar;
    public Text nameText;
    public ParentConstraint constraint;

    public void Init(C_Attributes attributes)
    {
        _attributes = attributes;
        hpBar.fillAmount = attributes.HPMax / attributes.HP;
        //nameText.text = attributes.playerName;
        constraint.SetSource(0, new ConstraintSource()
        {
            sourceTransform = attributes.nodHead,
            weight = 1
        }
);
    }

}
