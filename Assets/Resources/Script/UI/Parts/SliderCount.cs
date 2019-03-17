using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderCount : MonoBehaviour {
    public Text count;

    public void UpdateCount(Slider slider)
    {
        count.text = ((int)(slider.value * 100f)).ToString();
    }
}
