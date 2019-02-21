using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UITextData : UIData
{
    public Text text;
    // Use this for initialization
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((int)dataType == 1)
        {
            text.text = stringData.value;
        }else if ((int)dataType == 2)
        {
            text.text = floatData.value.ToString();
        }
    }
}
