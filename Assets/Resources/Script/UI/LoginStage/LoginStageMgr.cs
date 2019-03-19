using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class LoginStageMgr : MonoBehaviour {
    public InputField inputField;
    public Button loginBtn;
    public DOTweenAnimation blackAnimation;
    public void InputCheck()
    {
        inputField.text = inputField.text.Replace(" ", "");
        loginBtn.interactable = inputField.text.Length > 0;
    }

	public void LoginCheck()
    {
        var pName = inputField.text;
        if (pName.Length > 0)
        {
            Battle.playerBasicSave.playerName = pName;

            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + "/My Games/Impact Division";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            Battle.SavePlayerBasicData();
            Battle.SavePlayerBattleData();

            Battle.systemSettingSave = new SystemSetting();
            Battle.SaveSystemSettingData();
            
            blackAnimation.DOPlay();

        }
    }

    public void LoadMainStage()
    {
        SceneManager.LoadScene("MainStage");
    }
}
