using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchOption : SettingOption
{
    public bool interactable = true;
    [Header("[Extra --------------------]")]
    public Text keyName;
    public Button leftArrow;
    public Button rightArrow;
    public Text content;
    public int currentIndex = 0;
    public int originalIndex = 0;
    public List<string> options;
    public Action OnChangeCall;
    public NetworkEvent OnChange;
    
    public void SetInteractable(bool interactable = true)
    {
        this.interactable = interactable;

        keyName.color = interactable ? Color.white : Color.gray;
        content.color = interactable ? Color.white : Color.gray;

        leftArrow.interactable = interactable;
        rightArrow.interactable = interactable;

    }

    public void SetValue(int index)
    {
        content.text = options[index];
        this.originalIndex = index;
        this.currentIndex = index;
        leftArrow.onClick.AddListener(ArrowLeftClick);
        rightArrow.onClick.AddListener(ArrowRightClick);
    }

    public void ResetSetting()
    {
        content.text = options[this.originalIndex];
        this.currentIndex = this.originalIndex;
    }

    public void ArrowLeftClick()
    {
        currentIndex--;
        if (currentIndex < 0)
        {
            currentIndex = options.Count - 1;
        }
        if (options[currentIndex] != null)
        {
            content.text = options[currentIndex];
            this.changed = true;
        }
        OnChange.Invoke();
        OnChangeCall?.Invoke();
    }
    public void ArrowRightClick()
    {
        currentIndex++;
        if (currentIndex >= options.Count)
        {
            currentIndex = 0;
        }
        if (options[currentIndex] != null)
        {
            content.text = options[currentIndex];
            this.changed = true;
        }
        OnChange.Invoke();
        OnChangeCall?.Invoke();
    }
}
