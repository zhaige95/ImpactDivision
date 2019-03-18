using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSelectItem : MonoBehaviour {
    public RawImage image;
    public Text wName;

    public void UpdateData(ConfigWeapon config)
    {
        this.image.texture = config.cutPicInEquip;
        this.wName.text = config.wname;
    }

}
