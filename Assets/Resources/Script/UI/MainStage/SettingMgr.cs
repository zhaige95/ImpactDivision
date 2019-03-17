using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingMgr : MonoBehaviour {
    public List<GameObject> settingList;
    // Use this for initialization
    void Start () {
		
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

}
