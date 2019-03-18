using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchOption : SettingOption
{
    [Header("[Extra --------------------]")]
    public Text keyName;
    public Button leftArrow;
    public Button rightArrow;
    public Text content;
    public int currentIndex = 0;
    public int originalIndex = 0;
    public List<string> options;

    private void Awake()
    {
        content.text = options[0];

        leftArrow.onClick.AddListener(ArrowLeftClick);
        rightArrow.onClick.AddListener(ArrowRightClick);
    }

    public void ArrowLeftClick()
    {
        currentIndex--;
        currentIndex = Mathf.Clamp(currentIndex, 0, options.Count - 1);
        if (options[currentIndex] != null)
        {
            content.text = options[currentIndex];
            this.changed = true;
        }

    }
    public void ArrowRightClick()
    {
        currentIndex++;
        currentIndex = Mathf.Clamp(currentIndex, 0, options.Count - 1);
        if (options[currentIndex] != null)
        {
            content.text = options[currentIndex];
            this.changed = true;
        }
    }
}
