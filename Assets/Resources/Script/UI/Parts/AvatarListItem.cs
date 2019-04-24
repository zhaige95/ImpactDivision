using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvatarListItem : MonoBehaviour {
    public RawImage image;
    public Text avatarName;
    public ConfigAvatar config;

    public Action<ConfigAvatar> action;

    public void Init(ConfigAvatar config)
    {
        image.texture = config.headImage;
        avatarName.text = config.aName;
        this.config = config;
    }

    public void OnClick()
    {
        action?.Invoke(config);
    }

}
