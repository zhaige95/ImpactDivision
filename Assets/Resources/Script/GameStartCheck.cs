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
        string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + "/My Games/Impact Division";

        Battle.savePath = path;

        if (!Directory.Exists(path))
        {
            Battle.playerBasicSave = new PlayerBasic();
            Battle.playerBattleSave = new PlayerBattle();
            Battle.login = false;
        }
        else
        {
            var str = File.ReadAllText(path + "/PlayerBasic.cfg");
            Battle.playerBasicSave = JsonConvert.DeserializeObject<PlayerBasic>(str);
            str = File.ReadAllText(path + "/PlayerBattle.cfg");
            Battle.playerBattleSave = JsonConvert.DeserializeObject<PlayerBattle>(str);
            str = File.ReadAllText(path + "/SystemSetting.cfg");
            Battle.systemSettingSave = JsonConvert.DeserializeObject<SystemSetting>(str);

            Battle.login = true;
            //SceneManager.LoadScene("MainStage");
        }

        SceneManager.LoadScene("LoginStage");
        //var str = File.ReadAllText(Application.dataPath + "/Resources/Config/json/PlayerBasic.json");
        //PlayerBasic playerBasic = JsonConvert.DeserializeObject<PlayerBasic>(str);
        //Debug.Log(playerBasic.playerName);
        //Debug.Log(playerBasic.exp);

        //Battle.playerBasicSave = new PlayerBasic()
        //{
        //    playerName = "爱u说的话"
        //};
        //string str = JsonConvert.SerializeObject(Battle.playerBasicSave);
        //Debug.Log(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments));

        //string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + "/My Games/Impact Division";
        //if (!Directory.Exists(path))
        //{
        //    Directory.CreateDirectory(path);
        //}
        //File.WriteAllText(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments)+ "/My Games/Impact Division/PlayerBasic.json", str);

    }

    // Update is called once per frame
    void Update () {
		
	}
}
