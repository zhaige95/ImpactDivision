using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderOption : SettingOption {

    [Header("[Extra --------------------]")]
    public Slider slider;
    public Text countText;
    public void UpdateCount(Slider slider)
    {
        countText.text = ((int)(slider.value * 100f)).ToString();
        this.changed = true;
    }

    public void Init(float value)
    {
        slider.value = value;
        countText.text = value.ToString();
    }

}
