using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingMgr : WindowBasic
{
    public Button applyBtn;
    public List<GameObject> settingList;
    public GameObject confirmWindow;
    public SoundMgr soundMgr;
    // Use this for initialization
    public override void Init () {
        soundMgr.OnChanged = ActableApplyBtn;

        soundMgr.Init();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OpenSetting(int index)
    {
        for (int i = 0; i < settingList.Count; i++)
        {
            if (i == index)
                settingList[i].SetActive(true);
            else
                settingList[i].SetActive(false);
        }
    }

    public void ActableApplyBtn()
    {
        applyBtn.interactable = true;
    }

    public void ApplyChanges()
    {
        soundMgr.ApplyChanges();

        Battle.SaveSystemSettingData();
        applyBtn.interactable = false;
    }

    public void Back()
    {
        if (applyBtn.interactable)
        {
            confirmWindow.SetActive(true);
        }
        else
        {
            this.GetComponentInParent<MenuMgr>().OpenMenu(0);
        }
    }

    public void ResetSetting()
    {
        soundMgr.InitSetting();
        applyBtn.interactable = false;
    }
}
