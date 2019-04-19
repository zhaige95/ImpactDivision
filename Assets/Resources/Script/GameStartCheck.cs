using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using UnityEngine.SceneManagement;

public class GameStartCheck : MonoBehaviour {
	// Use this for initialization
	void Start () {
        //string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + "/My Games/Impact Division";
        string path = Application.dataPath + "/My Games/Impact Division";

        Battle.savePath = path;
        // 判断是否有存档
        if (Directory.Exists(path))
        {
            if (File.Exists(path + "/PlayerBasic.cfg") && File.Exists(path + "/PlayerBattle.cfg") && File.Exists(path + "/SystemSetting.cfg"))
            {
                LoginWithPLayerInfo(path);
            }
            else
            {
                LoadLoginStage();
            }
        }
        else
        {
            LoadLoginStage();
        }

        Battle.relativeRate = Screen.height / 1080f;

    }

    // Update is called once per frame
    void Update () {
		
	}

    public void LoginWithPLayerInfo(string path)
    {
        // 有存档，进入主界面
        var str = File.ReadAllText(path + "/PlayerBasic.cfg");
        Battle.playerBasicSave = JsonConvert.DeserializeObject<PlayerBasic>(str);
        str = File.ReadAllText(path + "/PlayerBattle.cfg");
        Battle.playerBattleSave = JsonConvert.DeserializeObject<PlayerBattle>(str);
        str = File.ReadAllText(path + "/SystemSetting.cfg");
        Battle.systemSettingSave = JsonConvert.DeserializeObject<SystemSetting>(str);

        Battle.login = true;
        SceneManager.LoadScene("MainStage");
    }

    public void LoadLoginStage()
    {
        // 没有存档进入登陆界面
        Battle.playerBasicSave = new PlayerBasic();
        Battle.playerBattleSave = new PlayerBattle();
        Battle.login = false;
        SceneManager.LoadScene("LoginStage");
    }

}
