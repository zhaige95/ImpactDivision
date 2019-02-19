using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Data;
using System;

public class UIFillAmountData : UIData
{
    public Image image;
    // Use this for initialization
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((int)dataType == 1)
        {
            image.fillAmount = Convert.ToSingle(stringData.value); 
        }else if ((int)dataType == 2)
        {
            image.fillAmount = floatData.value;
        }
    }
}
