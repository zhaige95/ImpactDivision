using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuInBattle : PanelSwitch {

    public NetworkEvent OnOpenSettingPanel;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void SwitchPanel(bool isOpen)
    {
        base.SwitchPanel(isOpen);
        if (isOpen)
        {
            OnOpenSettingPanel.Invoke();
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
}
