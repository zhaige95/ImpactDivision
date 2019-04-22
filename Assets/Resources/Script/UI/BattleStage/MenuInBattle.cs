using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuInBattle : PanelSwitch {

    public Button backBtn;
    public Button applyBtn;
    public GameObject confirmWindow;
    public NetworkEvent OnOpenSettingPanel;
    public NetworkEvent OnCloseSettingPanel;

    public override void SwitchPanel(bool isOpen)
    {
        if (isOpen)
        {
            base.SwitchPanel(isOpen);
            OnOpenSettingPanel.Invoke();

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Debug.Log("open setting panel");
        }
        else
        {
            Back();
            Debug.Log("close setting panel");
        }
    }

    public void SwitchItem(RectTransform trans, bool option)
    {
        trans.localScale = option ? Vector3.one : Vector3.zero;
    }

    public void CloseItem(RectTransform trans)
    {
        SwitchItem(trans, false);
    }

    public void OpenItem(RectTransform trans)
    {
        SwitchItem(trans, true);
    }
    
    public void Back()
    {
        if (applyBtn.interactable)
        {
            confirmWindow.SetActive(true);
        }
        else
        {
            OnCloseSettingPanel.Invoke();

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
