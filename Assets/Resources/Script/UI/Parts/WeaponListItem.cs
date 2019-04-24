using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponListItem : MonoBehaviour {
    public byte type;
    public RawImage cutPic;
    public Text wName;
    public Text damage;
    public Text fireSpeed;
    public Text mag;
    public Text spread;
    public ConfigWeapon config;
    public Action<ConfigWeapon, byte> OnClickCall;

    public void Init(ConfigWeapon config, byte type)
    {
        this.config = config;
        this.type = type;
        this.cutPic.texture = config.cutPicInEquip;
        this.wName.text = config.wname;
        this.damage.text = config.damage.ToString();
        this.fireSpeed.text = config.fireSpeed.ToString();
        this.mag.text = config.mag.ToString();
        this.spread.text = config.spread.ToString();
    }

    public void OnClick()
    {
        OnClickCall?.Invoke(this.config, this.type);

    }
}
