using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderOption : SettingOption {

    [Header("[Extra --------------------]")]
    public Slider slider;
    public Text countText;
    public Action<float> OnChanged;
    public void UpdateCount(Slider slider)
    {
        countText.text = ((int) ((1 - Math.Abs(slider.value / (slider.maxValue - slider.minValue))) * 100f)).ToString();
        OnChanged?.Invoke(slider.value);
        this.changed = true;
    }

    public void Init(float value)
    {
        slider.value = value * 0.01f * (slider.maxValue - slider.minValue) + slider.minValue;
        countText.text = value.ToString();
        OnChanged?.Invoke(slider.value);
    }



}
